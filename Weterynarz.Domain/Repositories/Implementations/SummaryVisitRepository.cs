using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;
using Weterynarz.Domain.ViewModels.AnimalTypes;
using Microsoft.Extensions.Logging;
using Weterynarz.Domain.ViewModels.SummaryVisit;
using Weterynarz.Domain.ViewModels.Visit;
using System.Data.Entity;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class SummaryVisitRepository : BaseRepository<SummaryVisit>, ISummaryVisitRepository
    {
        private ILogger<SummaryVisitRepository> _logger;
        private IMedicalExaminationTypesRepository _medicalExaminationTypesRepository;
        private IDiseasesRepository _diseasesRepository;
        private IVisitRepository _visitRepository;
        private IPriceListRepository _priceListRepository;

        public SummaryVisitRepository(ApplicationDbContext db, ILogger<SummaryVisitRepository> logger,
            IMedicalExaminationTypesRepository medicalExaminationTypesRepository,
            IDiseasesRepository diseasesRepository,
            IPriceListRepository priceListRepository,
            IVisitRepository visitRepository) : base(db)
        {
            _logger = logger;
            _medicalExaminationTypesRepository = medicalExaminationTypesRepository;
            _diseasesRepository = diseasesRepository;
            _visitRepository = visitRepository;
            _priceListRepository = priceListRepository;
        }

        public IEnumerable<SelectListItem> GetSummaryVisitSelectList()
        {
            throw new NotImplementedException();
        }

        public SummaryVisitIndexViewModel GetIndexViewModel(int visitId)
        {
            SummaryVisitIndexViewModel model;
            //var summaryVisit = base.GetById(id);
            var visit = _visitRepository.GetById(visitId, new string[] { "SummaryVisit", "Animal.Owner", "Animal.AnimalDiseases.Disease", "Animal.AnimalMedicalExaminations.MedicalExamination" });

            if (visit.SummaryVisit != null)
            {
                var diseases = visit.Animal.AnimalDiseases?.Select(m => m.Disease).ToList();
                var medicalExamination = visit.Animal.AnimalMedicalExaminations?.Select(m => m.MedicalExamination).ToList();

                model = new SummaryVisitIndexViewModel
                {
                    Active = visit.SummaryVisit.Active,
                    Animal = visit.Animal.Name,
                    Diseases = diseases?.Select(x => x.Name),
                    Drugs = visit.SummaryVisit.Drugs,
                    MedicalExaminations = medicalExamination?.Select(x => x.Name),
                    Owner = visit.Animal.Owner.Name + " " + visit.Animal.Owner.Surname,
                    VisitDate = visit.VisitDate,
                    Id = visit.SummaryVisit.Id,
                    Description = visit.SummaryVisit.Description,
                };

                return model;
            }

            return null;
        }

        public SummaryVisitManageViewModel GetCreateViewModel(int visitId, SummaryVisitManageViewModel model = null)
        {
            if(model == null)
            {
                model = new SummaryVisitManageViewModel();
                model.VisitId = visitId;
            }

            model.DiseaseSelectList = _diseasesRepository.GetDiseasesSelectList();
            model.MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList();
            model.AdditionalCostsSelectList = _priceListRepository.GetPriceListSelectList();

            return model;
        }

        public async Task CreateNew(SummaryVisitManageViewModel model)
        {
            var visit = _visitRepository.GetById(model.VisitId, new string[] { "Doctor", "Animal", "SummaryVisit" });

            SummaryVisit summary = new SummaryVisit
            {
                Drugs = model.Drugs,
                Active = true,
                CreationDate = DateTime.Now,
                Description = model.Description,
                VisitId = model.VisitId,
                Visit = visit,
                Price = model.Price
            };

            var animal = visit.Animal;
            if (animal == null)
            {
                throw new Exception(ResAdmin.visitRepo_animalNotFoundException);
            }

            var diseasesCollection = _db.Diseases.AsNoTracking().Where(d => model.DiseaseIds.Contains(d.Id)).ToList();
            var existingDiseases = _db.AnimalDiseases.AsNoTracking().Where(x => x.AnimalId == animal.Id).Select(x => x.Disease).ToList();
            var diseasesToAddList = diseasesCollection.Except(existingDiseases).ToList();
            foreach (var disease in diseasesToAddList)
            {
                animal.AnimalDiseases.Add(new AnimalDisease { Disease = disease, Animal = animal });
            }
            var diseasesToRemoveList = existingDiseases.Except(diseasesCollection).ToList();
            foreach (var disease in diseasesToRemoveList)
            {
                animal.AnimalDiseases.Remove(animal.AnimalDiseases.First(x => x.Disease == disease));
            }

            var medicalExaminationCollection = _db.MedicalExaminationTypes.AsNoTracking().Where(d => model.MedicalExaminationIds.Contains(d.Id)).ToList();
            var existingMedicalExaminations = _db.AnimalMedicalExaminations.AsNoTracking().Where(x => x.AnimalId == animal.Id).Select(x => x.MedicalExamination).ToList();
            var medicalExaminationToAddList = medicalExaminationCollection.Except(existingMedicalExaminations).ToList();
            foreach (var medicalExamination in medicalExaminationToAddList)
            {
                animal.AnimalMedicalExaminations.Add(new AnimalMedicalExamination { MedicalExamination = medicalExamination, Animal = animal });
            }
            var medicalExaminationToRemoveList = existingMedicalExaminations.Except(medicalExaminationCollection).ToList();
            foreach (var medicalExamination in medicalExaminationToRemoveList)
            {
                animal.AnimalMedicalExaminations.Remove(animal.AnimalMedicalExaminations.First(x => x.MedicalExamination == medicalExamination));
            }

            await base.InsertAsync(summary);
        }

        public SummaryVisitManageViewModel GetEditViewModel(int id)
        {
            SummaryVisitManageViewModel model;
            var summaryVisit = base.GetById(id);
            if (summaryVisit != null)
            {
                model = new SummaryVisitManageViewModel
                {
                    Active = summaryVisit.Active,
                    Drugs = summaryVisit.Drugs,
                    MedicalExaminationSelectList = _medicalExaminationTypesRepository.GetMedicalExaminationSelectList(),
                    DiseaseSelectList = _diseasesRepository.GetDiseasesSelectList(),
                    AdditionalCostsSelectList = _priceListRepository.GetPriceListSelectList(),
                    Id = summaryVisit.Id,
                    Description = summaryVisit.Description,
                    Price = summaryVisit.Price
                };

                return model;
            }

            return null;
        }

        public async Task<bool> Edit(SummaryVisitManageViewModel model)
        {
            var summaryVisit = base.GetById(model.Id);
            if (summaryVisit != null)
            {
                var animal = summaryVisit.Visit.Animal;
                if(animal == null)
                {
                    throw new Exception(ResAdmin.visitRepo_animalNotFoundException);
                }

                var diseasesCollection = _db.Diseases.AsNoTracking().Where(d => model.DiseaseIds.Contains(d.Id)).ToList();                
                var existingDiseases = animal.AnimalDiseases.Select(x => x.Disease).ToList();
                var diseasesToAddList = diseasesCollection.Except(existingDiseases).ToList();
                foreach (var disease in diseasesToAddList)
                {
                    animal.AnimalDiseases.Add(new AnimalDisease { Disease = disease, Animal = animal });
                }
                var diseasesToRemoveList = existingDiseases.Except(diseasesCollection).ToList();
                foreach (var disease in diseasesToRemoveList)
                {
                    animal.AnimalDiseases.Remove(animal.AnimalDiseases.First(x => x.Disease == disease));
                }

                var medicalExaminationCollection = _db.MedicalExaminationTypes.AsNoTracking().Where(d => model.MedicalExaminationIds.Contains(d.Id)).ToList();
                var existingMedicalExaminations = animal.AnimalMedicalExaminations.Select(x => x.MedicalExamination).ToList();
                var medicalExaminationToAddList = medicalExaminationCollection.Except(existingMedicalExaminations).ToList();
                foreach (var medicalExamination in medicalExaminationToAddList)
                {
                    animal.AnimalMedicalExaminations.Add(new AnimalMedicalExamination { MedicalExamination = medicalExamination, Animal = animal });
                }
                var medicalExaminationToRemoveList = existingMedicalExaminations.Except(medicalExaminationCollection).ToList();
                foreach (var medicalExamination in medicalExaminationToRemoveList)
                {
                    animal.AnimalMedicalExaminations.Remove(animal.AnimalMedicalExaminations.First(x => x.MedicalExamination == medicalExamination));
                }

                summaryVisit.Active = model.Active;
                summaryVisit.Drugs = model.Drugs;
                summaryVisit.Description = model.Description;
                summaryVisit.ModificationDate = DateTime.Now;
                summaryVisit.Price = model.Price;

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var summaryVisit = base.GetById(id);
            if (summaryVisit != null)
            {
                await base.SoftDeleteAsync(summaryVisit);
                return true;
            }

            return false;
        }
    }
}
