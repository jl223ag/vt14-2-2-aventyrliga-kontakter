using AventyrligaKontakter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AventyrligaKontakter
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SavedContact"] as bool? == true)
            {
                SaveMessage.Visible = true;
                Session.Remove("SavedContact");
            }
        }

        public IEnumerable<Contact> AdventurousListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void AdventurousListView_InsertItem(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveContact(contact);
                    Session["SavedContact"] = true;
                    Response.Redirect("~/Default.aspx"); // ny get av sidan för att undvika dubbelpostning (PRG)
                }

                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Något blev fel vid sparande av ny kontakt");
                }
            }
        }

        public void AdventurousListView_UpdateItem(int contactid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contact = Service.GetContactById(contactid);
                    if (contact != null)
                    {
                        if (TryUpdateModel(contact)) // sköter valideringen också
                        {
                            Service.SaveContact(contact);
                            SaveMessage.Visible = true;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, String.Format("Kontakten med kontaktnummer {0} hittades inte", contactid));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Något blev fel vid uppdatering av kontakt");
                }
            }
        }

        public void AdventurousListView_DeleteItem(int contactid)
        {
            try
            {
                Service.DeleteContact(contactid);
            }
            catch
            {
                ModelState.AddModelError(String.Empty, "Något blev fel vid borttagning av kontakt");
            }
        }
    }
}