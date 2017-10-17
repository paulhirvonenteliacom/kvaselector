using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kvaselector.Models;
using kvaselector.Viewmodels;
using System.ComponentModel;
using System.IO;
using GemBox.Spreadsheet;
using OfficeOpenXml;

namespace kvaselector.Controllers
{
    public class TreatmentsController : Controller
    {
        private kvaselectorContext db = new kvaselectorContext();

        public int qtyDiaSelectors = 2;
        public int qtyTreSelectors = 24;
        public int qtyCheckedTreSelectors = 4;

        //GET: Treatments/ServiceSelector
        [HttpGet]
        public ActionResult ServiceSelector()
        {
            ServiceSelectorVM serviceSelectorVM = new ServiceSelectorVM();

            List<SelectListItem> serviceTypes = new List<SelectListItem>();

            serviceTypes = db.ServiceTypes.ToList().ConvertAll(s => new SelectListItem
            {
                Value = $"{s.Id}",
                Text = s.Name
            });
            serviceSelectorVM.ServiceType = new SelectList(serviceTypes, "Value", "Text");

            serviceSelectorVM.ApplicationPurpose = "Denna applikation är ett hjälpmedel för att välja KVÅ-koder enligt rapporteringsanvisningen:";
            serviceSelectorVM.SourceDocumentTitleLine1 = "Stockholms läns landsting";
            serviceSelectorVM.SourceDocumentTitleLine2 = "Vårdval Logopedi";
            serviceSelectorVM.SourceDocumentTitleLine3 = "- rapporteringsanvisning gällande underlag för utbetalning av ersättning";
            serviceSelectorVM.SourceValidFrom = "Rapporteringsanvisningen gäller fr. o. m. 2017-01-01";
            serviceSelectorVM.SourceComplianceStatement = "Applikationen är kompatibel med uppdateringar gjorda i anvisningen t. o. m. 2017-09-04.";

            return View("ServiceSelector", serviceSelectorVM);
        }

        //GET: Treatments/CodeSelectorII
        [HttpGet]
        public ActionResult CodeSelectorII(ServiceSelectorVM serviceSelectorVM)
        {
            //Check that a service type has been selected
            if (serviceSelectorVM.SelectedServiceTypeId == null)
            {
                ModelState.AddModelError("ServiceType", "En vårdtjänst måste väljas.");
            }

            if (ModelState.IsValid)
            {
                //Find the selected service type
                var selectedServiceType = db.ServiceTypes.Find(serviceSelectorVM.SelectedServiceTypeId);

                //Find the service where ServiceType = Id of the selected service type
                var service = db.Services.Where(s => s.ServiceType == selectedServiceType.Id).FirstOrDefault();

                //Find the latest stored code combination for the service
                FavoriteService existingFavoriteService = new FavoriteService();

                //Load the viewmodel with the relevant data for the selected service type
                CodeSelectorIIVM codeSelectorIIVM = new CodeSelectorIIVM();

                codeSelectorIIVM.PageHeader = "Kodval";
                codeSelectorIIVM.ServiceName = selectedServiceType.Name;
                codeSelectorIIVM.ServiceDescription = selectedServiceType.Description;
                codeSelectorIIVM.SelectedServiceTypeId = selectedServiceType.Id;

                //Populate favorite dropdownlist
                //Check what favorites have been stored (if any) for this service

                List<SelectListItem> storedFavoriteServices = new List<SelectListItem>();

                storedFavoriteServices = db.FavoriteServices.Where(f => f.ServiceTypeId == codeSelectorIIVM.SelectedServiceTypeId).ToList().ConvertAll(f => new SelectListItem
                {
                    Value = $"{f.Id}",
                    Text = db.FavoriteNames.Find(f.FavoriteNameId).Name
                });
                codeSelectorIIVM.Favorite = new SelectList(storedFavoriteServices, "Value", "Text");
                codeSelectorIIVM.FavoriteHeading = "Snabbval för denna vårdtjänst";

                //Check if there are any stored favorite services with stored code combinations (FavoriteNameID must not be 4 or 5)
                bool storedFavoriteServicesWithCodesExists = false;
                var storedFavoriteServicesWithCodes = db.FavoriteServices.Where(f => f.ServiceTypeId == codeSelectorIIVM.SelectedServiceTypeId).
                                                            Where(f => f.FavoriteNameId != 4).Where(f => f.FavoriteNameId != 5).ToList();
                if (storedFavoriteServicesWithCodes.Count() > 0)
                {
                    storedFavoriteServicesWithCodesExists = true;
                    existingFavoriteService = storedFavoriteServicesWithCodes.First();
                }

                codeSelectorIIVM.DiagnosisCodeRemark = service.DiagnosisCodeRemark;
                codeSelectorIIVM.NoOfDiagnosisCodes = service.NoOfDiagnosisCodes;

                List<SelectListItem>[] diagnoses = new List<SelectListItem>[service.NoOfDiagnosisCodes];
                codeSelectorIIVM.Diagnosis = new SelectList[service.NoOfDiagnosisCodes];
                codeSelectorIIVM.DiagnosisCodeHeading = new string[service.NoOfDiagnosisCodes];
                codeSelectorIIVM.DiagnosisCodeHeading[0] = "Huvuddiagnos";
                codeSelectorIIVM.DiagnosisCodeHeading[1] = "Bidiagnos";

                //Check if there is a stored code combination
                int?[] preSelectedDiaCodes = new int?[service.NoOfDiagnosisCodes];
                int?[] preSelectedTreCodes = new int?[service.NoOfTreatmentCodes];

                if (storedFavoriteServicesWithCodesExists)
                {
                    preSelectedDiaCodes = System.Web.Helpers.Json.Decode<int?[]>(existingFavoriteService.SelectedDiagnosisCodeId);
                    preSelectedTreCodes = System.Web.Helpers.Json.Decode<int?[]>(existingFavoriteService.SelectedTreatmentCodeId);
                }
                else
                {
                    for (int i = 0; i < service.NoOfDiagnosisCodes; i++)
                    {
                        preSelectedDiaCodes[i] = null;
                    }
                    for (int i = 0; i < service.NoOfTreatmentCodes; i++)
                    {
                        preSelectedTreCodes[i] = null;
                    }
                }

                //Load diagnosis code dropdownlists. Include pre-selected value if it exists
                for (int i = 0; i < service.NoOfDiagnosisCodes; i++)
                {
                    diagnoses[i] = db.Diagnosis.ToList().ConvertAll(d => new SelectListItem
                    {
                        Value = $"{d.Id}",
                        Text = d.Code
                    });
                    codeSelectorIIVM.Diagnosis[i] = new SelectList(diagnoses[i], "Value", "Text");
                }
                codeSelectorIIVM.PreSelectedDiagnosisId = preSelectedDiaCodes;

                codeSelectorIIVM.TreatmentCodeRemark = service.TreatmentCodeRemark;
                codeSelectorIIVM.NoOfTreatmentCodes = service.NoOfTreatmentCodes;

                List<SelectListItem>[] treatments = new List<SelectListItem>[service.NoOfTreatmentCodes];
                codeSelectorIIVM.Treatment = new SelectList[service.NoOfTreatmentCodes];
                codeSelectorIIVM.TreatmentCodeHeading = new string[service.NoOfTreatmentCodes];
                //int?[] codeSelectorIIVM.PreSelectedTreatmentId = new int?[service.NoOfTreatmentCodes];

                //Parse treatment type string into an array of integers
                var treatmentTypeId = service.TreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

                //Load treatment code dropdownlists
                for (int i = 0; i < service.NoOfTreatmentCodes; i++)
                {
                    int treTypeId = treatmentTypeId[i];
                    treatments[i] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList().ConvertAll(t => new SelectListItem
                    {
                        Value = $"{t.Id}",
                        Text = t.Code
                    });
                    codeSelectorIIVM.Treatment[i] = new SelectList(treatments[i], "Value", "Text");
                    codeSelectorIIVM.TreatmentCodeHeading[i] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
                }
                codeSelectorIIVM.PreSelectedTreatmentId = preSelectedTreCodes;

                codeSelectorIIVM.NoOfCheckedTreatmentLists = service.NoOfCheckedTreatmentLists;
                List<Treatment>[] checkboxTreatments = new List<Treatment>[service.NoOfCheckedTreatmentLists];
                codeSelectorIIVM.CheckedTreatment = new List<TreatmentCheckBox>[service.NoOfCheckedTreatmentLists];
                codeSelectorIIVM.CheckedTreatmentHeading = new string[service.NoOfCheckedTreatmentLists];

                for (int i = 0; i < service.NoOfCheckedTreatmentLists; i++)
                {
                    codeSelectorIIVM.CheckedTreatment[i] = new List<TreatmentCheckBox>();
                }

                //Parse treatment type string into an array of integers
                var checkboxTreatmentTypeId = service.CheckedTreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

                //Load treatment code lists for checkboxes
                for (int i = 0; i < service.NoOfCheckedTreatmentLists; i++)
                {
                    int treTypeId = checkboxTreatmentTypeId[i];
                    checkboxTreatments[i] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList();

                    foreach (var item in checkboxTreatments[i])
                    {
                        TreatmentCheckBox treatmentCheckbox = new TreatmentCheckBox();
                        treatmentCheckbox.CheckboxTreatment = item.Code;
                        treatmentCheckbox.Checked = false;
                        codeSelectorIIVM.CheckedTreatment[i].Add(treatmentCheckbox);
                    }
                    codeSelectorIIVM.CheckedTreatmentHeading[i] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
                }

                //Check if there is a previously stored combination for this service
                //int?[] diagnosisCodesIds = System.Web.Helpers.Json.Decode<int?[]>(diaCodeIds);
                //int?[] treatmentCodesIds = System.Web.Helpers.Json.Decode<int?[]>(treCodeIds);
                //string[] checkedtreatmentCodes = System.Web.Helpers.Json.Decode<string[]>(checkedTreCodes);

                //Pre-select the latest stored combination of selected checkboxes
                if (storedFavoriteServicesWithCodesExists)
                {
                    string[] checkedtreatmentCodes = new string[qtyCheckedTreSelectors];
                    int[] checkedCodeIndex = new int[24];
                    checkedtreatmentCodes = System.Web.Helpers.Json.Decode<string[]>(existingFavoriteService.SelectedCheckedTreatmentCodeId);

                    for (int i = 0; i < codeSelectorIIVM.NoOfCheckedTreatmentLists; i++)
                    {
                        //Parse checkedtreatmentCodes to arrays with only one integer in each position
                        checkedtreatmentCodes[i] = checkedtreatmentCodes[i].TrimEnd(',');
                        checkedCodeIndex = checkedtreatmentCodes[i].Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();
                        for (int j = 0; j < checkedCodeIndex.Count(); j++)
                        {
                            codeSelectorIIVM.CheckedTreatment[i][checkedCodeIndex[j]].Checked = true;
                        }
                    }
                }
                return View("CodeSelectorII", codeSelectorIIVM);
            }

            //Redo service selection if model state not valid
            List<SelectListItem> serviceTypes = new List<SelectListItem>();

            serviceTypes = db.ServiceTypes.ToList().ConvertAll(s => new SelectListItem
            {
                Value = $"{s.Id}",
                Text = s.Name
            });
            serviceSelectorVM.ServiceType = new SelectList(serviceTypes, "Value", "Text");

            serviceSelectorVM.ApplicationPurpose = "Denna applikation är ett hjälpmedel för att välja KVÅ-koder enligt rapporteringsanvisningen:";
            serviceSelectorVM.SourceDocumentTitleLine1 = "Stockholms läns landsting";
            serviceSelectorVM.SourceDocumentTitleLine2 = "Vårdval Logopedi";
            serviceSelectorVM.SourceDocumentTitleLine3 = "- rapporteringsanvisning gällande underlag för utbetalning av ersättning";
            serviceSelectorVM.SourceValidFrom = "Rapporteringsanvisningen gäller fr. o. m. 2017-01-01";
            serviceSelectorVM.SourceComplianceStatement = "Applikationen är kompatibel med uppdateringar gjorda i anvisningen t. o. m. 2017-09-04.";

            return View("ServiceSelector", serviceSelectorVM);
        }

        //POST: Treatments/CodeSelector
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CodeSelectorII(int serviceId, string diagnosisCodes, string treatmentCodes, string checkedTreatmentCodes)
        //{
        //    string[] diagnosisCodeIds = System.Web.Helpers.Json.Decode<string[]>(diagnosisCodes);
        //    string[] treatmentCodeIds = System.Web.Helpers.Json.Decode<string[]>(treatmentCodes);
        //    string[] checkedTreatmentCodeIds = System.Web.Helpers.Json.Decode<string[]>(checkedTreatmentCodes);

        //    int?[] selectedDiagnosisIds = new int?[4];
        //    int?[] selectedTreatmentIds = new int?[16];
        //    List<int>[] selectedCheckedTreatmentIds = new List<int>[4];

        //    //Try to parse int Ids from arrays of strings.
        //    for (int idx = 0; idx < 4; idx++)
        //    {

        //    }

        //    //Basic validation
        //    //Check that at least one diagnosis code has been selected
        //    bool atLeastOneDiagnosisCode = false;
        //    int i = 0;
        //    while (!atLeastOneDiagnosisCode && i < codeSelectorIIVM.NoOfDiagnosisCodes)
        //    {
        //        if (codeSelectorIIVM.SelectedDiagnosisId[i] != null)
        //        {
        //            atLeastOneDiagnosisCode = true;
        //        }
        //        i++;
        //    }
        //    if (!atLeastOneDiagnosisCode)
        //    {
        //        ModelState.AddModelError("Diagnosis[0]", "Minst en diagnoskod måste väljas.");
        //    }

        //    //If more than one diagnosis codes have been selected, check if two are equal. If equal, then display an error message.
        //    if (atLeastOneDiagnosisCode)
        //    {
        //        bool moreThanOneDiagnosisCodeSelected = false;
        //        int idx = 0;
        //        int diagnosisCounter = 0;
        //        while (!moreThanOneDiagnosisCodeSelected && idx < codeSelectorIIVM.NoOfDiagnosisCodes)
        //        {
        //            if (codeSelectorIIVM.SelectedDiagnosisId[i] != null)
        //            {
        //                diagnosisCounter++;
        //                if (diagnosisCounter > 1)
        //                {
        //                    moreThanOneDiagnosisCodeSelected = true;
        //                }
        //            }
        //            idx++;
        //        }
        //        int matchIdx = 0;
        //        string matchCode = "";
        //        if (moreThanOneDiagnosisCodeSelected)
        //        {
        //            bool twoCodesEqual = false;
        //            int diaidx = 0;
        //            while (!twoCodesEqual && diaidx < codeSelectorIIVM.NoOfDiagnosisCodes - 1)
        //            {
        //                if (codeSelectorIIVM.SelectedDiagnosisId[diaidx] != null)
        //                {
        //                    for (int diaidx2 = diaidx + 1; diaidx2 < codeSelectorIIVM.NoOfDiagnosisCodes; diaidx2++)
        //                    {
        //                        if (codeSelectorIIVM.SelectedDiagnosisId[diaidx2] != null)
        //                        {
        //                            if (codeSelectorIIVM.SelectedDiagnosisId[diaidx] == codeSelectorIIVM.SelectedDiagnosisId[diaidx2])
        //                            {
        //                                twoCodesEqual = true;
        //                                matchIdx = diaidx;
        //                            }
        //                        }
        //                    }
        //                }
        //                diaidx++;
        //            }
        //            if (twoCodesEqual)
        //            {
        //                matchCode = db.Diagnoses.Find(codeSelectorIIVM.SelectedDiagnosisId[matchIdx]).Code.Substring(0, 6);
        //                ModelState.AddModelError("Diagnosis[0]", "Kod " + matchCode + " har valts två gånger." + " Det är inte tillåtet att välja två lika diagnoskoder.");
        //            }
        //        }
        //    }

        //    //Check that at least one treatment code has been selected
        //    //Wait with validation of treatment codes until I know more about which combinations are required.
        //    //bool atLeastOneTreatmentCode = false;
        //    //int j = 0;
        //    //while (!atLeastOneTreatmentCode && j < codeSelectorIIVM.NoOfTreatmentCodes)
        //    //{
        //    //    if (codeSelectorIIVM.SelectedTreatmentId[j] != null)
        //    //    {
        //    //        atLeastOneTreatmentCode = true;
        //    //    }
        //    //    j++;
        //    //}
        //    //if (!atLeastOneTreatmentCode)
        //    //{
        //    //    ModelState.AddModelError("Type[0]", "Minst en åtgärdskod måste väljas.");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        ConfirmCodesVM confirmCodesVM = new ConfirmCodesVM();

        //        confirmCodesVM.PageHeader = "Granska kodval";

        //        //Store diagnosis codes in a list of strings.
        //        confirmCodesVM.SelectedDiagnosisCodes = new List<string>();
        //        confirmCodesVM.SelectedDiagnosisDescriptions = new List<string>();
        //        var selectedDiagnoses = new List<string>();
        //        int dcounter = 0;
        //        for (int dIdx = 0; dIdx < codeSelectorIIVM.NoOfDiagnosisCodes; dIdx++)
        //        {
        //            if (codeSelectorIIVM.SelectedDiagnosisId[dIdx] != null)
        //            {
        //                selectedDiagnoses.Add(db.Diagnoses.Find(codeSelectorIIVM.SelectedDiagnosisId[dIdx]).Code);
        //                dcounter++;
        //            }
        //        }
        //        foreach (var item in selectedDiagnoses)
        //        {
        //            confirmCodesVM.SelectedDiagnosisCodes.Add(item.Substring(0, 6));
        //            confirmCodesVM.SelectedDiagnosisDescriptions.Add(item.Substring(8));
        //        }
        //        confirmCodesVM.NoOfDiagnosisCodes = dcounter;
        //        confirmCodesVM.DiagnosisHeader = "Diagnoskoder" + " " + "(" + dcounter.ToString() + " st)";

        //        //Store treatment codes in a list of strings.
        //        confirmCodesVM.SelectedTreatmentCodes = new List<string>();
        //        confirmCodesVM.SelectedTreatmentDescriptions = new List<string>();
        //        var selectedTreatments = new List<string>();
        //        int tcounter = 0;
        //        for (int tIdx = 0; tIdx < codeSelectorIIVM.NoOfTreatmentCodes; tIdx++)
        //        {
        //            if (codeSelectorIIVM.SelectedTreatmentId[tIdx] != null)
        //            {
        //                selectedTreatments.Add(db.Treatments.Find(codeSelectorIIVM.SelectedTreatmentId[tIdx]).Code);
        //                tcounter++;
        //            }
        //        }
        //        foreach (var item in selectedTreatments)
        //        {
        //            confirmCodesVM.SelectedTreatmentCodes.Add(item.Substring(0, 5));
        //            confirmCodesVM.SelectedTreatmentDescriptions.Add(item.Substring(7));
        //        }
        //        confirmCodesVM.NoOfTreatmentCodes = tcounter;
        //        confirmCodesVM.TreatmentHeader = "Åtgärdskoder" + " " + "(" + tcounter.ToString() + " st)";

        //        confirmCodesVM.SelectedDiagnosisId = codeSelectorIIVM.SelectedDiagnosisId;
        //        confirmCodesVM.Diagnosis = codeSelectorIIVM.Diagnosis;
        //        confirmCodesVM.SelectedTypeId = codeSelectorIIVM.SelectedTreatmentId;
        //        confirmCodesVM.Type = codeSelectorIIVM.Treatment;

        //        confirmCodesVM.noOfDiaCodePositionsInOutput = 4;
        //        confirmCodesVM.noOfTreCodePositionsInOutput = 24;

        //        return View("ConfirmCodes", confirmCodesVM);
        //    }

        //    //Find the selected service type
        //    var selectedServiceType = db.ServiceTypes.Find(codeSelectorIIVM.SelectedServiceTypeId);

        //    //Find the service where ServiceType = Id of the selected service type
        //    var service = db.Services.Where(s => s.ServiceType == selectedServiceType.Id).FirstOrDefault();

        //    //Load the viewmodel with the relevant data for the selected service type
        //    //CodeSelectorIIVM codeSelectorIIVM = new CodeSelectorIIVM();

        //    codeSelectorIIVM.PageHeader = "Kodval";
        //    codeSelectorIIVM.ServiceName = selectedServiceType.Name;
        //    codeSelectorIIVM.ServiceDescription = selectedServiceType.Description;

        //    codeSelectorIIVM.DiagnosisCodeRemark = service.DiagnosisCodeRemark;
        //    codeSelectorIIVM.NoOfDiagnosisCodes = service.NoOfDiagnosisCodes;

        //    List<SelectListItem>[] diagnoses = new List<SelectListItem>[service.NoOfDiagnosisCodes];
        //    codeSelectorIIVM.Diagnosis = new SelectList[service.NoOfDiagnosisCodes];
        //    codeSelectorIIVM.DiagnosisCodeHeading = new string[service.NoOfDiagnosisCodes];
        //    codeSelectorIIVM.DiagnosisCodeHeading[0] = "Huvuddiagnos";
        //    codeSelectorIIVM.DiagnosisCodeHeading[1] = "Bidiagnos";

        //    //Load diagnosis code dropdownlists
        //    for (int idx = 0; idx < service.NoOfDiagnosisCodes; idx++)
        //    {
        //        diagnoses[idx] = db.Diagnoses.ToList().ConvertAll(d => new SelectListItem
        //        {
        //            Value = $"{d.Id}",
        //            Text = d.Code
        //        });
        //        codeSelectorIIVM.Diagnosis[idx] = new SelectList(diagnoses[idx], "Value", "Text");
        //    }

        //    codeSelectorIIVM.TreatmentCodeRemark = service.TreatmentCodeRemark;
        //    codeSelectorIIVM.NoOfTreatmentCodes = service.NoOfTreatmentCodes;

        //    List<SelectListItem>[] treatments = new List<SelectListItem>[service.NoOfTreatmentCodes];
        //    codeSelectorIIVM.Treatment = new SelectList[service.NoOfTreatmentCodes];
        //    codeSelectorIIVM.TreatmentCodeHeading = new string[service.NoOfTreatmentCodes];

        //    //Parse treatment type string into an array of integers
        //    var treatmentTypeId = service.TreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

        //    //Load treatment code dropdownlists
        //    for (int idx = 0; idx < service.NoOfTreatmentCodes; idx++)
        //    {
        //        int treTypeId = treatmentTypeId[idx];
        //        treatments[idx] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList().ConvertAll(t => new SelectListItem
        //        {
        //            Value = $"{t.Id}",
        //            Text = t.Code
        //        });
        //        codeSelectorIIVM.Treatment[idx] = new SelectList(treatments[idx], "Value", "Text");
        //        codeSelectorIIVM.TreatmentCodeHeading[idx] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
        //    }

        //    codeSelectorIIVM.NoOfCheckedTreatmentLists = service.NoOfCheckedTreatmentLists;
        //    List<bool>[] checkedTreatments = new List<bool>[service.NoOfCheckedTreatmentLists];

        //    List<Treatment>[] checkboxTreatments = new List<Treatment>[service.NoOfCheckedTreatmentLists];
        //    codeSelectorIIVM.CheckedTreatment = new List<Treatment>[service.NoOfCheckedTreatmentLists];
        //    codeSelectorIIVM.CheckedTreatmentHeading = new string[service.NoOfCheckedTreatmentLists];
        //    codeSelectorIIVM.Checked = new List<bool>[service.NoOfCheckedTreatmentLists];

        //    for (int idx = 0; idx < service.NoOfCheckedTreatmentLists; idx++)
        //    {
        //        checkedTreatments[idx] = new List<bool>();
        //    }

        //    //Parse treatment type string into an array of integers
        //    var checkboxTreatmentTypeId = service.CheckedTreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

        //    //Load treatment code lists for checkboxes
        //    for (int idx = 0; idx < service.NoOfCheckedTreatmentLists; idx++)
        //    {
        //        int treTypeId = checkboxTreatmentTypeId[idx];
        //        checkboxTreatments[idx] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList();
        //        for (int j = 0; j < checkboxTreatments[i].Count(); j++)
        //        {
        //            checkedTreatments[idx].Add(false);
        //        }
        //        codeSelectorIIVM.CheckedTreatment[idx] = checkboxTreatments[idx];
        //        codeSelectorIIVM.Checked[idx] = checkedTreatments[idx];
        //        codeSelectorIIVM.CheckedTreatmentHeading[idx] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
        //    }

        //    return View("CodeSelectorII", codeSelectorIIVM);
        //}

        //POST: Treatments/CodeSelectorII
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CodeSelectorII(CodeSelectorIIVM codeSelectorIIVM)
        {
            //Basic validation
            //Check that at least one diagnosis code has been selected
            bool atLeastOneDiagnosisCode = false;
            int i = 0;
            while (!atLeastOneDiagnosisCode && i < codeSelectorIIVM.NoOfDiagnosisCodes)
            {
                if (codeSelectorIIVM.SelectedDiagnosisId[i] != null)
                {
                    atLeastOneDiagnosisCode = true;
                }
                i++;
            }
            if (!atLeastOneDiagnosisCode)
            {
                ModelState.AddModelError("Diagnosis[0]", "Minst en diagnoskod måste väljas.");
            }

            //If more than one diagnosis codes have been selected, check if two are equal. If equal, then display an error message.
            if (atLeastOneDiagnosisCode)
            {
                bool moreThanOneDiagnosisCodeSelected = false;
                int idx = 0;
                int diagnosisCounter = 0;
                while (!moreThanOneDiagnosisCodeSelected && idx < codeSelectorIIVM.NoOfDiagnosisCodes)
                {
                    if (codeSelectorIIVM.SelectedDiagnosisId[i] != null)
                    {
                        diagnosisCounter++;
                        if (diagnosisCounter > 1)
                        {
                            moreThanOneDiagnosisCodeSelected = true;
                        }
                    }
                    idx++;
                }
                int matchIdx = 0;
                string matchCode = "";
                if (moreThanOneDiagnosisCodeSelected)
                {
                    bool twoCodesEqual = false;
                    int diaidx = 0;
                    while (!twoCodesEqual && diaidx < codeSelectorIIVM.NoOfDiagnosisCodes - 1)
                    {
                        if (codeSelectorIIVM.SelectedDiagnosisId[diaidx] != null)
                        {
                            for (int diaidx2 = diaidx + 1; diaidx2 < codeSelectorIIVM.NoOfDiagnosisCodes; diaidx2++)
                            {
                                if (codeSelectorIIVM.SelectedDiagnosisId[diaidx2] != null)
                                {
                                    if (codeSelectorIIVM.SelectedDiagnosisId[diaidx] == codeSelectorIIVM.SelectedDiagnosisId[diaidx2])
                                    {
                                        twoCodesEqual = true;
                                        matchIdx = diaidx;
                                    }
                                }
                            }
                        }
                        diaidx++;
                    }
                    if (twoCodesEqual)
                    {
                        matchCode = db.Diagnosis.Find(codeSelectorIIVM.SelectedDiagnosisId[matchIdx]).Code.Substring(0, 6);
                        ModelState.AddModelError("Diagnosis[0]", "Kod " + matchCode + " har valts två gånger." + " Det är inte tillåtet att välja två lika diagnoskoder.");
                    }
                }
            }

            //WORK IN PROGRESS
            //Check that at least one treatment code has been selected
            //Wait with validation of treatment codes until I know more about which combinations are required.
            //bool atLeastOneTreatmentCode = false;
            //int j = 0;
            //while (!atLeastOneTreatmentCode && j < codeSelectorIIVM.NoOfTreatmentCodes)
            //{
            //    if (codeSelectorIIVM.SelectedTreatmentId[j] != null)
            //    {
            //        atLeastOneTreatmentCode = true;
            //    }
            //    j++;
            //}
            //if (!atLeastOneTreatmentCode)
            //{
            //    ModelState.AddModelError("Type[0]", "Minst en åtgärdskod måste väljas.");
            //}

            //Find the selected service type
            var selectedServiceType = db.ServiceTypes.Find(codeSelectorIIVM.SelectedServiceTypeId);

            //Find the service where ServiceType = Id of the selected service type
            var service = db.Services.Where(s => s.ServiceType == selectedServiceType.Id).FirstOrDefault();

            //Parse treatment type string into an array of integers
            var checkboxTreatmentTypeId = service.CheckedTreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

            List<Treatment>[] checkboxTreatments = new List<Treatment>[service.NoOfCheckedTreatmentLists];

            if (ModelState.IsValid)
            {
                ConfirmCodesVM confirmCodesVM = new ConfirmCodesVM();

                confirmCodesVM.PageHeader = "Granska kodval";

                //Store diagnosis codes in a list of strings.
                confirmCodesVM.SelectedDiagnosisCodes = new List<string>();
                confirmCodesVM.SelectedDiagnosisDescriptions = new List<string>();
                var selectedDiagnoses = new List<string>();
                int dcounter = 0;

                for (int dIdx = 0; dIdx < codeSelectorIIVM.NoOfDiagnosisCodes; dIdx++)
                {
                    if (codeSelectorIIVM.SelectedDiagnosisId[dIdx] != null)
                    {
                        selectedDiagnoses.Add(db.Diagnosis.Find(codeSelectorIIVM.SelectedDiagnosisId[dIdx]).Code);
                        dcounter++;
                    }
                }
                foreach (var item in selectedDiagnoses)
                {
                    confirmCodesVM.SelectedDiagnosisCodes.Add(item.Substring(0, 6));
                    confirmCodesVM.SelectedDiagnosisDescriptions.Add(item.Substring(8));
                }
                confirmCodesVM.NoOfDiagnosisCodes = dcounter;
                confirmCodesVM.DiagnosisHeader = "Diagnoskoder" + " " + "(" + dcounter.ToString() + " st)";

                //Store treatment codes in a list of strings.
                confirmCodesVM.SelectedTreatmentCodes = new List<string>();
                confirmCodesVM.SelectedTreatmentDescriptions = new List<string>();
                var selectedTreatments = new List<string>();
                int tcounter = 0;

                for (int tIdx = 0; tIdx < codeSelectorIIVM.NoOfTreatmentCodes; tIdx++)
                {
                    if (codeSelectorIIVM.SelectedTreatmentId[tIdx] != null)
                    {
                        selectedTreatments.Add(db.Treatments.Find(codeSelectorIIVM.SelectedTreatmentId[tIdx]).Code);
                        tcounter++;
                    }
                }
                foreach (var item in selectedTreatments)
                {
                    confirmCodesVM.SelectedTreatmentCodes.Add(item.Substring(0, 5));
                    confirmCodesVM.SelectedTreatmentDescriptions.Add(item.Substring(7));
                }

                //WORK IN PROGRESS
                //These lines may only be needed when a cancel button will be implemented 
                //confirmCodesVM.SelectedDiagnosisId = codeSelectorIIVM.SelectedDiagnosisId;
                //confirmCodesVM.Diagnosis = codeSelectorIIVM.Diagnosis;
                //confirmCodesVM.SelectedTypeId = codeSelectorIIVM.SelectedTreatmentId;
                //confirmCodesVM.Type = codeSelectorIIVM.Treatment;

                //Store checkbox treatment codes in a list of strings.
                //Load treatment code lists for checkboxes

                for (int idx = 0; idx < codeSelectorIIVM.NoOfCheckedTreatmentLists; idx++)
                {
                    int treTypeId = checkboxTreatmentTypeId[idx];
                    checkboxTreatments[idx] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList();

                    for (int idx2 = 0; idx2 < checkboxTreatments[idx].Count(); idx2++)
                    {
                        //Check if the treatment code was selected. If true, add to list of codes to be confirmed.
                        if (codeSelectorIIVM.CheckedTreatment[idx][idx2].Checked)
                        {
                            confirmCodesVM.SelectedTreatmentCodes.Add(checkboxTreatments[idx][idx2].Code.Substring(0, 5));
                            confirmCodesVM.SelectedTreatmentDescriptions.Add(checkboxTreatments[idx][idx2].Code.Substring(7));
                            tcounter++;
                        }
                    }
                }
                confirmCodesVM.NoOfTreatmentCodes = tcounter;
                confirmCodesVM.TreatmentHeader = "Åtgärdskoder" + " " + "(" + tcounter.ToString() + " st)";

                confirmCodesVM.noOfDiaCodePositionsInOutput = 4;
                confirmCodesVM.noOfTreCodePositionsInOutput = 24;

                //Store data in viewmodel for favorite service (later: SelectedTypeId in ConfirmedCodesVM should be renamed to SelectedTreatmentId)
                confirmCodesVM.SelectedDiagnosisId = new int?[codeSelectorIIVM.NoOfDiagnosisCodes];
                confirmCodesVM.SelectedTypeId = new int?[codeSelectorIIVM.NoOfTreatmentCodes];
                confirmCodesVM.TypeOfServiceId = codeSelectorIIVM.SelectedServiceTypeId;
                confirmCodesVM.SelectedDiagnosisId = codeSelectorIIVM.SelectedDiagnosisId;
                confirmCodesVM.SelectedTypeId = codeSelectorIIVM.SelectedTreatmentId;

                for (int idx = 0; idx < codeSelectorIIVM.NoOfCheckedTreatmentLists; idx++)
                {
                    confirmCodesVM.CheckedTreatment[idx] = "";
                    for (int idx2 = 0; idx2 < codeSelectorIIVM.CheckedTreatment[idx].Count(); idx2++)
                    {
                        if (codeSelectorIIVM.CheckedTreatment[idx][idx2].Checked)
                        {
                            confirmCodesVM.CheckedTreatment[idx] = confirmCodesVM.CheckedTreatment[idx] + idx2.ToString() + ",";
                        }
                    }
                }

                //WORK IN PROGRESS
                //var favoriteNames = db.FavoriteNames.Where(f => f.Id == 1 || f.Id == 2).ToList();
                //string[] favorite = new string[confirmCodesVM.NoOfRadioButtons];
                //for (int idx = 0; idx < confirmCodesVM.NoOfRadioButtons; idx++)
                //{
                //    favorite[idx] = favoriteNames[idx].Name;
                //}
                //confirmCodesVM.FavoriteChoice = new ConfirmCodesVM.FavoriteRadioButton[confirmCodesVM.NoOfRadioButtons];
                //for (int idx = 0; idx < confirmCodesVM.NoOfRadioButtons; idx++)
                //{
                //    ConfirmCodesVM.FavoriteRadioButton favoriteRadioButton = new ConfirmCodesVM.FavoriteRadioButton();
                //    favoriteRadioButton.FavoriteName = favorite[idx];
                //    favoriteRadioButton.Selected = false;
                //    confirmCodesVM.FavoriteChoice[idx] = favoriteRadioButton;
                //}

                //confirmCodesVM.FavoriteChoices = new ConfirmCodesVM.FavoriteRadioButtonII[confirmCodesVM.NoOfRadioButtons];
                //confirmCodesVM.SelectedFavorite = new bool[confirmCodesVM.NoOfRadioButtons];
                //for (int idx = 0; idx < confirmCodesVM.NoOfRadioButtons; idx++)
                //{
                //    ConfirmCodesVM.FavoriteRadioButtonII favoriteRadioButton = new ConfirmCodesVM.FavoriteRadioButtonII();
                //    favoriteRadioButton.FavoriteName = favorite[idx];
                //    favoriteRadioButton.Id = idx + 1;
                //    confirmCodesVM.FavoriteChoices[idx] = favoriteRadioButton;
                //    confirmCodesVM.SelectedFavorite[idx] = false;
                //}

                return View("ConfirmCodes", confirmCodesVM);
            }

            //Load the viewmodel with the relevant data for the selected service type

            codeSelectorIIVM.PageHeader = "Kodval";
            codeSelectorIIVM.ServiceName = selectedServiceType.Name;
            codeSelectorIIVM.ServiceDescription = selectedServiceType.Description;

            codeSelectorIIVM.DiagnosisCodeRemark = service.DiagnosisCodeRemark;
            codeSelectorIIVM.NoOfDiagnosisCodes = service.NoOfDiagnosisCodes;

            List<SelectListItem>[] diagnoses = new List<SelectListItem>[service.NoOfDiagnosisCodes];
            codeSelectorIIVM.Diagnosis = new SelectList[service.NoOfDiagnosisCodes];
            codeSelectorIIVM.DiagnosisCodeHeading = new string[service.NoOfDiagnosisCodes];
            codeSelectorIIVM.DiagnosisCodeHeading[0] = "Huvuddiagnos";
            codeSelectorIIVM.DiagnosisCodeHeading[1] = "Bidiagnos";

            //Load diagnosis code dropdownlists
            for (int idx = 0; idx < service.NoOfDiagnosisCodes; idx++)
            {
                diagnoses[idx] = db.Diagnosis.ToList().ConvertAll(d => new SelectListItem
                {
                    Value = $"{d.Id}",
                    Text = d.Code
                });
                codeSelectorIIVM.Diagnosis[idx] = new SelectList(diagnoses[idx], "Value", "Text");
            }

            codeSelectorIIVM.TreatmentCodeRemark = service.TreatmentCodeRemark;
            codeSelectorIIVM.NoOfTreatmentCodes = service.NoOfTreatmentCodes;

            List<SelectListItem>[] treatments = new List<SelectListItem>[service.NoOfTreatmentCodes];
            codeSelectorIIVM.Treatment = new SelectList[service.NoOfTreatmentCodes];
            codeSelectorIIVM.TreatmentCodeHeading = new string[service.NoOfTreatmentCodes];

            //Parse treatment type string into an array of integers
            var treatmentTypeId = service.TreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

            //Load treatment code dropdownlists
            for (int idx = 0; idx < service.NoOfTreatmentCodes; idx++)
            {
                int treTypeId = treatmentTypeId[idx];
                treatments[idx] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList().ConvertAll(t => new SelectListItem
                {
                    Value = $"{t.Id}",
                    Text = t.Code
                });
                codeSelectorIIVM.Treatment[idx] = new SelectList(treatments[idx], "Value", "Text");
                codeSelectorIIVM.TreatmentCodeHeading[idx] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
            }

            codeSelectorIIVM.NoOfCheckedTreatmentLists = service.NoOfCheckedTreatmentLists;
            //List<bool>[] checkedTreatments = new List<bool>[service.NoOfCheckedTreatmentLists];

            codeSelectorIIVM.CheckedTreatment = new List<TreatmentCheckBox>[service.NoOfCheckedTreatmentLists];
            codeSelectorIIVM.CheckedTreatmentHeading = new string[service.NoOfCheckedTreatmentLists];
            //codeSelectorIIVM.Checked = new List<bool>[service.NoOfCheckedTreatmentLists];

            for (int idx = 0; idx < service.NoOfCheckedTreatmentLists; idx++)
            {
                codeSelectorIIVM.CheckedTreatment[idx] = new List<TreatmentCheckBox>();
            }

            //Parse treatment type string into an array of integers
            //var checkboxTreatmentTypeId = service.CheckedTreatmentTypeId.Split(",".ToCharArray()).Select(x => int.Parse(x.ToString())).ToArray();

            //Load treatment code lists for checkboxes
            for (int idx = 0; idx < service.NoOfCheckedTreatmentLists; idx++)
            {
                int treTypeId = checkboxTreatmentTypeId[idx];
                checkboxTreatments[idx] = db.Treatments.Where(t => t.TreatmentTypeId == treTypeId).ToList();

                foreach (var item in checkboxTreatments[idx])
                {
                    TreatmentCheckBox treatmentCheckbox = new TreatmentCheckBox();
                    treatmentCheckbox.CheckboxTreatment = item.Name;
                    treatmentCheckbox.Checked = false;
                    codeSelectorIIVM.CheckedTreatment[idx].Add(treatmentCheckbox);
                }
                codeSelectorIIVM.CheckedTreatmentHeading[idx] = db.TreatmentTypes.Where(t => t.Id == treTypeId).FirstOrDefault().Name;
            }

            return View("CodeSelectorII", codeSelectorIIVM);
        }

        //GET: Treatments/CodeSelector
        //[HttpGet]
        //public ActionResult CodeSelector()
        //{
        //    CodeSelectorVM codeSelectorVM = new CodeSelectorVM();
        //    codeSelectorVM.Diagnosis = new SelectList[qtyDiaSelectors];
        //    codeSelectorVM.Type = new SelectList[qtyTreSelectors];
        //    codeSelectorVM.TypeName = new string[qtyTreSelectors];

        //    List<SelectListItem>[] diagnoses = new List<SelectListItem>[qtyDiaSelectors];

        //    for (int i = 0; i < qtyDiaSelectors; i++)
        //    {
        //        diagnoses[i] = db.Diagnoses.ToList().ConvertAll(d => new SelectListItem
        //        {
        //            Value = $"{d.Id}",
        //            Text = d.Code
        //        });
        //        codeSelectorVM.Diagnosis[i] = new SelectList(diagnoses[i], "Value", "Text");
        //    }

        //    List<SelectListItem>[] treatmentsType = new List<SelectListItem>[qtyTreSelectors];

        //    for (int i = 0; i < qtyTreSelectors; i++)
        //    {
        //        treatmentsType[i] = db.Treatments.Where(t => t.TreatmentTypeId == i + 1).Include(t => t.TreatmentType).ToList().ConvertAll(t => new SelectListItem
        //        {
        //            Value = $"{t.Id}",
        //            Text = t.Code
        //        });
        //        codeSelectorVM.Type[i] = new SelectList(treatmentsType[i], "Value", "Text");
        //    }

        //    codeSelectorVM.TypeName[0] = "Diagnostisk åtgärd";
        //    codeSelectorVM.TypeName[1] = "Behandlande åtgärd";
        //    codeSelectorVM.TypeName[2] = "Åtgärd utan patientbesök";
        //    codeSelectorVM.TypeName[3] = "Tilläggsuppgift";

        //    codeSelectorVM.Diagnosis = diagnoses;
        //    codeSelectorVM.NoOfDiagnosisCodes = qtyDiaSelectors;
        //    codeSelectorVM.NoOfTreatmentCodes = qtyTreSelectors;

        //    return View("CodeSelector", codeSelectorVM);
        //}

        //POST: Treatments/CodeSelector
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CodeSelector(CodeSelectorVM codeSelectorVM)
        //{
        //    //Basic validation
        //    //Check that at least one diagnosis code has been selected
        //    bool atLeastOneDiagnosisCode = false;
        //    int i = 0;
        //    while (!atLeastOneDiagnosisCode && i < qtyDiaSelectors)
        //    {
        //        if (codeSelectorVM.SelectedDiagnosisId[i] != null)
        //        {
        //            atLeastOneDiagnosisCode = true;
        //        }
        //        i++;
        //    }
        //    if (!atLeastOneDiagnosisCode)
        //    {
        //        ModelState.AddModelError("Diagnosis[0]", "Minst en diagnoskod måste väljas.");
        //    }

        //    //If more than one diagnosis codes have been selected, check if two are equal. If equal, then display an error message.
        //    if (atLeastOneDiagnosisCode)
        //    {
        //        bool moreThanOneDiagnosisCodeSelected = false;
        //        int idx = 0;
        //        int diagnosisCounter = 0;
        //        while (!moreThanOneDiagnosisCodeSelected && idx < qtyDiaSelectors)
        //        {
        //            if (codeSelectorVM.SelectedDiagnosisId[i] != null)
        //            {
        //                diagnosisCounter++;
        //                if (diagnosisCounter > 1)
        //                {
        //                    moreThanOneDiagnosisCodeSelected = true;
        //                }
        //            }
        //            idx++;
        //        }
        //        int matchIdx = 0;
        //        string matchCode = "";
        //        if (moreThanOneDiagnosisCodeSelected)
        //        {
        //            bool twoCodesEqual = false;
        //            int diaidx = 0;
        //            while (!twoCodesEqual && diaidx < qtyDiaSelectors - 1)
        //            {
        //                if (codeSelectorVM.SelectedDiagnosisId[diaidx] != null)
        //                {
        //                    for (int diaidx2 = diaidx + 1; diaidx2 < qtyDiaSelectors; diaidx2++)
        //                    {
        //                        if (codeSelectorVM.SelectedDiagnosisId[diaidx2] != null)
        //                        {
        //                            if (codeSelectorVM.SelectedDiagnosisId[diaidx] == codeSelectorVM.SelectedDiagnosisId[diaidx2])
        //                            {
        //                                twoCodesEqual = true;
        //                                matchIdx = diaidx;
        //                            }
        //                        }
        //                    }
        //                }
        //                diaidx++;
        //            }
        //            if (twoCodesEqual)
        //            {
        //                matchCode = db.Diagnoses.Find(codeSelectorVM.SelectedDiagnosisId[matchIdx]).Code.Substring(0, 6);
        //                ModelState.AddModelError("Diagnosis[0]", "Kod " + matchCode + " har valts två gånger." + " Det är inte tillåtet att välja två lika diagnoskoder.");
        //            }
        //        }
        //    }

        //    //Check that at least one treatment code has been selected
        //    bool atLeastOneTreatmentCode = false;
        //    int j = 0;
        //    while (!atLeastOneTreatmentCode && j < qtyTreSelectors)
        //    {
        //        if (codeSelectorVM.SelectedTypeId[j] != null)
        //        {
        //            atLeastOneTreatmentCode = true;
        //        }
        //        j++;
        //    }
        //    if (!atLeastOneTreatmentCode)
        //    {
        //        ModelState.AddModelError("Type[0]", "Minst en åtgärdskod måste väljas.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        ConfirmCodesVM confirmCodesVM = new ConfirmCodesVM();

        //        confirmCodesVM.PageHeader = "Granska kodval";
        //        //selectedDiagnoses.Add(db.Diagnoses.Find(codeSelectorVM.SelectedDiagnosisId).Code);

        //        //Store diagnosis codes in a list of strings.
        //        confirmCodesVM.SelectedDiagnosisCodes = new List<string>();
        //        confirmCodesVM.SelectedDiagnosisDescriptions = new List<string>();
        //        var selectedDiagnoses = new List<string>();
        //        int dcounter = 0;
        //        for (int dIdx = 0; dIdx < qtyDiaSelectors; dIdx++)
        //        {
        //            if (codeSelectorVM.SelectedDiagnosisId[dIdx] != null)
        //            {
        //                selectedDiagnoses.Add(db.Diagnoses.Find(codeSelectorVM.SelectedDiagnosisId[dIdx]).Code);
        //                dcounter++;
        //            }
        //        }
        //        foreach (var item in selectedDiagnoses)
        //        {
        //            confirmCodesVM.SelectedDiagnosisCodes.Add(item.Substring(0, 6));
        //            confirmCodesVM.SelectedDiagnosisDescriptions.Add(item.Substring(8));
        //        }
        //        confirmCodesVM.NoOfDiagnosisCodes = dcounter;
        //        confirmCodesVM.DiagnosisHeader = "Diagnoskoder" + " " + "(" + dcounter.ToString() + " st)";

        //        //Store treatment codes in a list of strings.
        //        confirmCodesVM.SelectedTreatmentCodes = new List<string>();
        //        confirmCodesVM.SelectedTreatmentDescriptions = new List<string>();
        //        var selectedTreatments = new List<string>();
        //        int tcounter = 0;
        //        for (int tIdx = 0; tIdx < qtyTreSelectors; tIdx++)
        //        {
        //            if (codeSelectorVM.SelectedTypeId[tIdx] != null)
        //            {
        //                selectedTreatments.Add(db.Treatments.Find(codeSelectorVM.SelectedTypeId[tIdx]).Code);
        //                tcounter++;
        //            }
        //        }
        //        foreach (var item in selectedTreatments)
        //        {
        //            confirmCodesVM.SelectedTreatmentCodes.Add(item.Substring(0, 5));
        //            confirmCodesVM.SelectedTreatmentDescriptions.Add(item.Substring(7));
        //        }
        //        confirmCodesVM.NoOfTreatmentCodes = tcounter;
        //        confirmCodesVM.TreatmentHeader = "Åtgärdskoder" + " " + "(" + tcounter.ToString() + " st)";

        //        confirmCodesVM.SelectedDiagnosisId = codeSelectorVM.SelectedDiagnosisId;
        //        confirmCodesVM.Diagnosis = codeSelectorVM.Diagnosis;
        //        confirmCodesVM.SelectedTypeId = codeSelectorVM.SelectedTypeId;
        //        confirmCodesVM.Type = codeSelectorVM.Type;

        //        confirmCodesVM.noOfDiaCodePositionsInOutput = 4;
        //        confirmCodesVM.noOfTreCodePositionsInOutput = 24;

        //        return View("ConfirmCodes", confirmCodesVM);
        //    }

        //    //ModelState.AddModelError("CodeSelector", "Model is Not Valid");
        //    //return View("CodeSelector", codeSelectorVM);

        //    List<SelectListItem>[] diagnoses = new List<SelectListItem>[qtyDiaSelectors];
        //    codeSelectorVM.Diagnosis = new SelectList[qtyDiaSelectors];
        //    for (int dindex = 0; dindex < qtyDiaSelectors; dindex++)
        //    {
        //        diagnoses[dindex] = db.Diagnoses.ToList().ConvertAll(d => new SelectListItem
        //        {
        //            Value = $"{d.Id}",
        //            Text = d.Code
        //        });
        //        codeSelectorVM.Diagnosis[dindex] = new SelectList(diagnoses[dindex], "Value", "Text");
        //    }

        //    List<SelectListItem>[] treatmentsType = new List<SelectListItem>[qtyTreSelectors];
        //    codeSelectorVM.Type = new SelectList[qtyTreSelectors];
        //    for (int tindex = 0; tindex < qtyTreSelectors; tindex++)
        //    {
        //        treatmentsType[tindex] = db.Treatments.Where(t => t.TreatmentTypeId == tindex + 1).Include(t => t.TreatmentType).ToList().ConvertAll(t => new SelectListItem
        //        {
        //            Value = $"{t.Id}",
        //            Text = t.Code
        //        });
        //        codeSelectorVM.Type[tindex] = new SelectList(treatmentsType[tindex], "Value", "Text");
        //    }

        //    codeSelectorVM.TypeName = new string[qtyTreSelectors];
        //    codeSelectorVM.TypeName[0] = "Diagnostisk åtgärd";
        //    codeSelectorVM.TypeName[1] = "Behandlande åtgärd";
        //    codeSelectorVM.TypeName[2] = "Åtgärd utan patientbesök";
        //    codeSelectorVM.TypeName[3] = "Tilläggsuppgift";

        //    return View("CodeSelector", codeSelectorVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SaveConfirmedCodes(ConfirmCodesVM confirmCodesVM)
        //{
        //    return RedirectToAction("ServiceSelector", "Treatments");
        //}

        public ActionResult SaveConfirmedCodes(int noOfDiaCodes, string diaCodes, string diaDescriptions,
                int noOfTreCodes, string treCodes, string treDescriptions, int serviceTypeId, string diaCodeIds, string treCodeIds,
                string checkedTreCodes, int noOfRadioButtons, string radioButtons, string selFavorite)
        {
            string[] diagnosisCodes = System.Web.Helpers.Json.Decode<string[]>(diaCodes);
            string[] diagnosisDescriptions = System.Web.Helpers.Json.Decode<string[]>(diaDescriptions);
            string[] treatmentCodes = System.Web.Helpers.Json.Decode<string[]>(treCodes);
            string[] treatmentDescriptions = System.Web.Helpers.Json.Decode<string[]>(treDescriptions);
            bool[] favoriteRadioButtonSelected = System.Web.Helpers.Json.Decode<bool[]>(radioButtons);
            string[] selectedFavorite = System.Web.Helpers.Json.Decode<string[]>(selFavorite);

            //int?[] diagnosisCodesIds = System.Web.Helpers.Json.Decode<int?[]>(diaCodeIds);
            //int?[] treatmentCodesIds = System.Web.Helpers.Json.Decode<int?[]>(treCodeIds);
            //string[] checkedtreatmentCodes = System.Web.Helpers.Json.Decode<string[]>(checkedTreCodes);

            int favoriteNameId;

            //WORK IN PROGRESS
            ////Update favorites if favorite radiobuttons selected
            //for (int i = 0; i < noOfRadioButtons; i++)
            //{
            //    if (favoriteRadioButtonSelected[i])
            //    {
            //        UpdateFavorite(serviceTypeId, favoriteNameId = i + 1, diaCodeIds, treCodeIds, checkedTreCodes);
            //    }
            //}
            //Always update lastest stored combination
            UpdateFavorite(serviceTypeId, favoriteNameId = 3, diaCodeIds, treCodeIds, checkedTreCodes);

            //qtyTreSelectors is used for now, possibly later change to qtyDiaSelectors
            //Fill unused code and description indexes with empty strings 
            for (int i = noOfDiaCodes; i < qtyDiaSelectors; i++)
            {
                diagnosisCodes[i] = "";
                diagnosisDescriptions[i] = "";
            }
            for (int i = noOfTreCodes; i < qtyTreSelectors; i++)
            {
                treatmentCodes[i] = "";
                treatmentDescriptions[i] = "";
            }
            CreateExcelWithCodes(noOfDiaCodes, diagnosisCodes, diagnosisDescriptions, noOfTreCodes, treatmentCodes, treatmentDescriptions);
            //ExportListUsingEPPlus(noOfDiaCodes, diagnosisCodes, noOfTreCodes, treatmentCodes);

            //ExportListFromTsv(diagnosisCode, treatmentCodes);
            //If ExportListFromTsv (which causes format error/warning) is not called, then the method WriteTsv is also obsolete

            return RedirectToAction("ServiceSelector", "Treatments");
        }

        private void UpdateFavorite(int serviceTypeId, int favoriteNameId, string diaCodeIds, string treCodeIds, string checkedTreCodes)
        {
            FavoriteService favoriteService = new FavoriteService();
            var existingLatestStoredFavoriteService = db.FavoriteServices.Where(f => f.ServiceTypeId == serviceTypeId).Where(f => f.FavoriteNameId == favoriteNameId).FirstOrDefault();

            //Remove the latest stored combination of codes from the database if there is one
            if (existingLatestStoredFavoriteService != null)
            {
                db.FavoriteServices.Remove(existingLatestStoredFavoriteService);
            }

            //Add this combination to the database as the latest stored for this service
            favoriteService.FavoriteNameId = 3;
            favoriteService.ServiceTypeId = serviceTypeId;
            favoriteService.SelectedDiagnosisCodeId = diaCodeIds;
            favoriteService.SelectedTreatmentCodeId = treCodeIds;
            favoriteService.SelectedCheckedTreatmentCodeId = checkedTreCodes;
            db.FavoriteServices.Add(favoriteService);
            db.SaveChanges();
        }

        // GET: Treatments
        public ActionResult Index()
        {
            //var treatments = db.Treatments.Include("TreatmentType").ToList();
            var treatments = db.Treatments.Include(t => t.TreatmentType);

            TreatmentsOverviewVM treatmentsOverview = new TreatmentsOverviewVM();

            List<Treatment> treatmentsType1 = new List<Treatment>();
            List<Treatment> treatmentsType2 = new List<Treatment>();
            List<Treatment> treatmentsType3 = new List<Treatment>();
            List<Treatment> treatmentsType4 = new List<Treatment>();

            foreach (var treatment in treatments)
            {
                switch (treatment.TreatmentTypeId)
                {
                    case 1:
                        treatmentsType1.Add(treatment);
                        break;
                    case 2:
                        treatmentsType2.Add(treatment);
                        break;
                    case 3:
                        treatmentsType3.Add(treatment);
                        break;
                    case 4:
                        treatmentsType4.Add(treatment);
                        break;
                    default:
                        break;
                }
            }

            treatmentsOverview.Type1Name = treatmentsType1[1].TreatmentType.Name;
            treatmentsOverview.Type1 = treatmentsType1;

            treatmentsOverview.Type2Name = treatmentsType2[1].TreatmentType.Name;
            treatmentsOverview.Type2 = treatmentsType2;

            treatmentsOverview.Type3Name = treatmentsType3[1].TreatmentType.Name;
            treatmentsOverview.Type3 = treatmentsType3;

            treatmentsOverview.Type4Name = treatmentsType4[1].TreatmentType.Name;
            treatmentsOverview.Type4 = treatmentsType4;

            return View("Index", treatmentsOverview);
        }

        // GET: Treatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TreatmentTypeId,Code,Name,Description")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatments.Add(treatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TreatmentTypeId,Code,Name,Description")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            db.Treatments.Remove(treatment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void ExportListUsingEPPlus(int noOfDiagnoses, string[] diagnosisCode, int noOfTreatments, string[] treatmentCode)
        {
            var data = new[]{
                                new{ Typ="Diagnos", Antal=noOfDiagnoses, Kod1=diagnosisCode[0],
                                Kod2 =diagnosisCode[1], Kod3=diagnosisCode[2], Kod4=diagnosisCode[3] },
                                new{ Typ="Åtgärd", Antal=noOfTreatments, Kod1=treatmentCode[0],
                                Kod2 =treatmentCode[1], Kod3=treatmentCode[2], Kod4=treatmentCode[3] }
                            };

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=selectedcodes.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void ExportListFromTsv(string diagnosis, string[] treatmentCode)
        {
            var data = new[]{
                                new{ Diagnoskod=diagnosis, Kod1=treatmentCode[0],
                                Kod2 =treatmentCode[1], Kod3=treatmentCode[2], Kod4=treatmentCode[3] }
                            };

            Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment;filepath=C:\\Users\\paulh\\OneDrive\\Documents\\Visual Studio 2017\\Projects\\kvacodeselector\\Selectedcodes;filename=selectedcodes.xlsx");
            Response.AddHeader("content-disposition", "attachment;filename=selectedcodes.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteTsv(data, Response.Output);
            Response.End();
        }

        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                    prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        //CreateExcelWithCodes writes codes into an excel file and saves the file
        public void CreateExcelWithCodes(int noOfDiagnoses, string[] diagnosisCode, string[] diagnosisDescription, int noOfTreatments, string[] treatmentCode, string[] treatmentDescription)
        {
            // If using Professional version, put your serial key below.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            ExcelFile ef = new ExcelFile();
            GemBox.Spreadsheet.ExcelWorksheet ws = ef.Worksheets.Add("Koder");

            //First row - header
            ws.Cells[0, 0].Value = "Typ";
            ws.Cells[0, 1].Value = "Antal";
            ws.Cells[0, 2].Value = "Kod 1";
            ws.Cells[0, 3].Value = "Beskrivning 1";
            ws.Cells[0, 4].Value = "Kod 2";
            ws.Cells[0, 5].Value = "Beskrivning 2";
            ws.Cells[0, 6].Value = "Kod 3";
            ws.Cells[0, 7].Value = "Beskrivning 3";
            ws.Cells[0, 8].Value = "Kod 4";
            ws.Cells[0, 9].Value = "Beskrivning 4";
            ws.Cells[0, 10].Value = "Kod 5";
            ws.Cells[0, 11].Value = "Beskrivning 5";
            ws.Cells[0, 12].Value = "Kod 6";
            ws.Cells[0, 13].Value = "Beskrivning 6";
            ws.Cells[0, 14].Value = "Kod 7";
            ws.Cells[0, 15].Value = "Beskrivning 7";
            ws.Cells[0, 16].Value = "Kod 8";
            ws.Cells[0, 17].Value = "Beskrivning 8";
            ws.Cells[0, 18].Value = "Kod 9";
            ws.Cells[0, 19].Value = "Beskrivning 9";
            ws.Cells[0, 20].Value = "Kod 10";
            ws.Cells[0, 21].Value = "Beskrivning 10";
            ws.Cells[0, 22].Value = "Kod 11";
            ws.Cells[0, 23].Value = "Beskrivning 11";
            ws.Cells[0, 24].Value = "Kod 12";
            ws.Cells[0, 25].Value = "Beskrivning 12";
            ws.Cells[0, 26].Value = "Kod 13";
            ws.Cells[0, 27].Value = "Beskrivning 13";
            ws.Cells[0, 28].Value = "Kod 14";
            ws.Cells[0, 29].Value = "Beskrivning 14";
            ws.Cells[0, 30].Value = "Kod 15";
            ws.Cells[0, 31].Value = "Beskrivning 15";
            ws.Cells[0, 32].Value = "Kod 16";
            ws.Cells[0, 33].Value = "Beskrivning 16";
            ws.Cells[0, 34].Value = "Kod 17";
            ws.Cells[0, 35].Value = "Beskrivning 17";
            ws.Cells[0, 36].Value = "Kod 18";
            ws.Cells[0, 37].Value = "Beskrivning 18";
            ws.Cells[0, 38].Value = "Kod 19";
            ws.Cells[0, 39].Value = "Beskrivning 19";
            ws.Cells[0, 40].Value = "Kod 20";
            ws.Cells[0, 41].Value = "Beskrivning 20";
            ws.Cells[0, 42].Value = "Kod 21";
            ws.Cells[0, 43].Value = "Beskrivning 21";
            ws.Cells[0, 44].Value = "Kod 22";
            ws.Cells[0, 45].Value = "Beskrivning 22";
            ws.Cells[0, 46].Value = "Kod 23";
            ws.Cells[0, 47].Value = "Beskrivning 23";
            ws.Cells[0, 48].Value = "Kod 24";
            ws.Cells[0, 49].Value = "Beskrivning 24";

            //Second row - diagnosis codes
            ws.Cells[1, 0].Value = "Diagnos";
            ws.Cells[1, 1].Value = noOfDiagnoses;
            ws.Cells[1, 2].Value = diagnosisCode[0];
            ws.Cells[1, 3].Value = diagnosisDescription[0];
            ws.Cells[1, 4].Value = diagnosisCode[1];
            ws.Cells[1, 5].Value = diagnosisDescription[1];
            ws.Cells[1, 6].Value = diagnosisCode[2];
            ws.Cells[1, 7].Value = diagnosisDescription[2];
            ws.Cells[1, 8].Value = diagnosisCode[3];
            ws.Cells[1, 9].Value = diagnosisDescription[3];

            //Thir row - treatment codes
            ws.Cells[2, 0].Value = "Åtgärd";
            ws.Cells[2, 1].Value = noOfTreatments;
            ws.Cells[2, 2].Value = treatmentCode[0];
            ws.Cells[2, 3].Value = treatmentDescription[0];
            ws.Cells[2, 4].Value = treatmentCode[1];
            ws.Cells[2, 5].Value = treatmentDescription[1];
            ws.Cells[2, 6].Value = treatmentCode[2];
            ws.Cells[2, 7].Value = treatmentDescription[2];
            ws.Cells[2, 8].Value = treatmentCode[3];
            ws.Cells[2, 9].Value = treatmentDescription[3];
            ws.Cells[2, 10].Value = treatmentCode[4];
            ws.Cells[2, 11].Value = treatmentDescription[4];
            ws.Cells[2, 12].Value = treatmentCode[5];
            ws.Cells[2, 13].Value = treatmentDescription[5];
            ws.Cells[2, 14].Value = treatmentCode[6];
            ws.Cells[2, 15].Value = treatmentDescription[6];
            ws.Cells[2, 16].Value = treatmentCode[7];
            ws.Cells[2, 17].Value = treatmentDescription[7];
            ws.Cells[2, 18].Value = treatmentCode[8];
            ws.Cells[2, 19].Value = treatmentDescription[8];
            ws.Cells[2, 20].Value = treatmentCode[9];
            ws.Cells[2, 21].Value = treatmentDescription[9];
            ws.Cells[2, 22].Value = treatmentCode[10];
            ws.Cells[2, 23].Value = treatmentDescription[10];
            ws.Cells[2, 24].Value = treatmentCode[11];
            ws.Cells[2, 25].Value = treatmentDescription[11];
            ws.Cells[2, 26].Value = treatmentCode[12];
            ws.Cells[2, 27].Value = treatmentDescription[12];
            ws.Cells[2, 28].Value = treatmentCode[13];
            ws.Cells[2, 29].Value = treatmentDescription[13];
            ws.Cells[2, 30].Value = treatmentCode[14];
            ws.Cells[2, 31].Value = treatmentDescription[14];
            ws.Cells[2, 32].Value = treatmentCode[15];
            ws.Cells[2, 33].Value = treatmentDescription[15];
            ws.Cells[2, 34].Value = treatmentCode[16];
            ws.Cells[2, 35].Value = treatmentDescription[16];
            ws.Cells[2, 36].Value = treatmentCode[17];
            ws.Cells[2, 37].Value = treatmentDescription[17];
            ws.Cells[2, 38].Value = treatmentCode[18];
            ws.Cells[2, 39].Value = treatmentDescription[18];
            ws.Cells[2, 40].Value = treatmentCode[19];
            ws.Cells[2, 41].Value = treatmentDescription[19];
            ws.Cells[2, 42].Value = treatmentCode[20];
            ws.Cells[2, 43].Value = treatmentDescription[20];
            ws.Cells[2, 44].Value = treatmentCode[21];
            ws.Cells[2, 45].Value = treatmentDescription[21];
            ws.Cells[2, 46].Value = treatmentCode[22];
            ws.Cells[2, 47].Value = treatmentDescription[22];
            ws.Cells[2, 48].Value = treatmentCode[23];
            ws.Cells[2, 49].Value = treatmentDescription[23];

            ef.Save("C:\\Users\\paulh\\OneDrive\\Documents\\Bitoreq AB\\APL\\logopeddok\\selectedcodes.xlsx");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
