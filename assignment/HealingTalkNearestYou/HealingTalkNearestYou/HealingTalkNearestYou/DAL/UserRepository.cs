using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealingTalkNearestYou.Models;

namespace HealingTalkNearestYou.DAL
{
    public class UserRepository
    {
        public AdminViewModel AdminValidate(LoginViewModel model)
        {
            HTNYContainer1 htny_DB = new HTNYContainer1();
            Admin admin = htny_DB.AdminSet.Where(a => a.AdminEmail == model.UserEmail
                && a.AdminPassword == model.UserPassword).FirstOrDefault();

            if (admin != null)
            {
                AdminViewModel adminViewModel = new AdminViewModel();
                adminViewModel.AdminEmail = admin.AdminEmail;
                return adminViewModel;
            }
            else
            {
                return null;
            }
        }

        public PatientViewModel PatientValidate(LoginViewModel model)
        {
            HTNYContainer1 htny_DB = new HTNYContainer1();
            Patient patient = htny_DB.PatientSet.Where(a => a.PatientEmail == model.UserEmail
                && a.PatientPassword == model.UserPassword).FirstOrDefault();

            if (patient != null)
            {
                PatientViewModel patientViewModel = new PatientViewModel();
                patientViewModel.PatientEmail = patient.PatientEmail;
                patientViewModel.PatientName = patient.PatientName;
                patientViewModel.PatientGender = patient.PatientGender;
                patientViewModel.PatientDOB = (DateTime)patient.PatientDOB;
                return patientViewModel;
            }
            else
            {
                return null;
            }
        }

        public PsychologistViewModel PsychologistValidate(LoginViewModel model)
        {
            HTNYContainer1 htny_DB = new HTNYContainer1();
            Psychologist psychologist = htny_DB.PsychologistSet.Where(a => a.PsyEmail == model.UserEmail
                && a.PsyPassword == model.UserPassword).FirstOrDefault();

            if (psychologist != null)
            {
                PsychologistViewModel psyViewModel = new PsychologistViewModel();
                psyViewModel.PsyEmail = psychologist.PsyEmail;
                psyViewModel.PsyName = psychologist.PsyName;
                psyViewModel.PsyGender = psychologist.PsyGender;
                psyViewModel.PsyDOB = (DateTime)psychologist.PsyDOB;
                return psyViewModel;
            }
            else
            {
                return null;
            }
        }

        public Admin AdminRegister(RegisterAdminViewModel model)
        {

            Admin admin = new Admin();
            admin.AdminEmail = model.Email;
            admin.AdminPassword = model.Password;

            return admin;
        }

        public Patient PatientRegister(RegisterNormalViewModel model)
        {

            Patient patient = new Patient();
            patient.PatientEmail = model.Email;
            patient.PatientGender = model.Gender;
            patient.PatientPassword = model.Password;
            patient.PatientDOB = model.DOB;
            patient.PatientName = model.Name;

            return patient;
        }

        public Psychologist PsychologistRegister(RegisterNormalViewModel model)
        {

            Psychologist psychologist = new Psychologist();
            psychologist.PsyEmail = model.Email;
            psychologist.PsyGender = model.Gender;
            psychologist.PsyPassword = model.Password;
            psychologist.PsyDOB = model.DOB;
            psychologist.PsyDescription = null;
            psychologist.PsyName = model.Name;

            return psychologist;
        }
    }
}