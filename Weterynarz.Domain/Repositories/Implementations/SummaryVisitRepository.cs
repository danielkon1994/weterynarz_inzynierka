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

        public SummaryVisitRepository(ApplicationDbContext db, ILogger<SummaryVisitRepository> logger,
            IMedicalExaminationTypesRepository medicalExaminationTypesRepository,
            IDiseasesRepository diseasesRepository,
            IVisitRepository visitRepository) : base(db)
        {
            _logger = logger;
            _medicalExaminationTypesRepository = medicalExaminationTypesRepository;
            _diseasesRepository = diseasesRepository;
            _visitRepository = visitRepository;
        }

        public IEnumerable<SelectListItem> GetSummaryVisitSelectList()
        {
            throw new NotImplementedException();
        }

        public SummaryVisitIndexViewModel GetIndexViewModel(int id)
        {
            SummaryVisitIndexViewModel model;
            //var summaryVisit = base.GetById(id);
            var summaryVisit = _db.SummaryVisits.Include(v => v.Visit).Where(i => !i.Deleted).FirstOrDefault(i => i.Id == id);
            if (summaryVisit != null)
            {
                model = new SummaryVisitIndexViewModel
                {
                    Active = summaryVisit.Active,
                    Animal = summaryVisit.Visit.Animal.Name,
                    Diseases = summaryVisit.Visit.Animal.AnimalDiseases.Select(m => m.Disease).ToList().Select(m => m.Name),
                    Drugs = summaryVisit.Drugs,
                    MedicalExaminations = summaryVisit.Visit.Animal.AnimalMedicalExaminations.Select(m => m.MedicalExamination).ToList().Select(m => m.Name),
                    Owner = summaryVisit.Visit.Animal.Owner.Name + " " + summaryVisit.Visit.Animal.Owner.Surname,
                    VisitDate = summaryVisit.Visit.VisitDate,
                    Id = summaryVisit.Id,
                    Description = summaryVisit.Description,
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

            return model;
        }

        public async Task CreateNew(SummaryVisitManageViewModel model)
        {
            var visit = _visitRepository.GetById(model.VisitId);

            SummaryVisit summary = new SummaryVisit
            {
                Drugs = model.Drugs,
                Active = true,
                CreationDate = DateTime.Now,
                Description = model.Description,
                Visit = visit
            };

            var animal = visit.Animal;
            if (animal == null)
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
                    Id = summaryVisit.Id,
                    Description = summaryVisit.Description,
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
