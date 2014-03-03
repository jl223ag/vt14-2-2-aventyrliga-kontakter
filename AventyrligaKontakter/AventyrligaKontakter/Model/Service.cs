using AventyrligaKontakter.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AventyrligaKontakter.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        public IEnumerable<Contact> GetContacts() // alla kontakter
        {
            return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public Contact GetContactById(int contactid) // enskild kontakt
        {
            return ContactDAL.GetContactById(contactid);
        }

        public void DeleteContact(int contactid) // ta bort
        {
            ContactDAL.DeleteContact(contactid);
        }

        public void DeleteContact(Contact contact)
        {
            DeleteContact(contact.ContactID);
        }

        public void SaveContact(Contact contact) // spara
        {
            ICollection<ValidationResult> validres;

            if (!contact.Validate(out validres))
            {
                var ex = new ValidationException("Det gick inte att spara kunden!!");
                ex.Data.Add("ValidationResults", validres);

                throw ex;                   
            }

            if (contact.ContactID == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            } 
        }
    }
}