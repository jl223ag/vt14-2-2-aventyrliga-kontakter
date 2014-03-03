using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AventyrligaKontakter.Model.DAL
{
    public class ContactDAL : DALBase
    {
        public IEnumerable<Contact> GetContacts()
        {
            using (var con = CS())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstnameIndex = reader.GetOrdinal("FirstName");
                        var lastnameIndex = reader.GetOrdinal("LastName");
                        var emailIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstnameIndex),
                                LastName = reader.GetString(lastnameIndex),
                                EmailAddress = reader.GetString(emailIndex)
                            });
                        }
                    }

                    contacts.TrimExcess();
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("Det blev något fel vid hämtningen av kontakter");
                }
            }
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var con = CS())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = (startRowIndex / maximumRows) + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    con.Open();

                    cmd.ExecuteNonQuery();
                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstnameIndex = reader.GetOrdinal("FirstName");
                        var lastnameIndex = reader.GetOrdinal("LastName");
                        var emailIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstnameIndex),
                                LastName = reader.GetString(lastnameIndex),
                                EmailAddress = reader.GetString(emailIndex)
                            });
                        }
                    }
                    contacts.TrimExcess();

                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("Det blev något fel vid hämtningen av kontakter");
                }
            }
        }
    
        public Contact GetContactById(int contactId)
        {
            using (var con = CS())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId; // parametern till den lagrade proceduren

                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstnameIndex = reader.GetOrdinal("FirstName");
                        var lastnameIndex = reader.GetOrdinal("LastName");
                        var emailIndex = reader.GetOrdinal("EmailAddress");

                        if (reader.Read())
                        {
                            return new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstnameIndex),
                                LastName = reader.GetString(lastnameIndex),
                                EmailAddress = reader.GetString(emailIndex)
                            };
                        }
                    }

                    return null;
                }
                catch
                {
                    throw new ApplicationException("Det gick inte att hämta kunden");
                }
            }
        }

        public void InsertContact(Contact contact)
        {
            using (var con = CS())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspAddContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    
                    con.Open();
                    cmd.ExecuteNonQuery();

                    contact.ContactID = (int)cmd.Parameters["@ContactID"].Value;
                }
                catch
                {
                    throw new ApplicationException("Det gick inte att spara kunden");
                }
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (var con = CS())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspUpdateContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Det gick inte att uppdatera kunden");
                }
            }
        }

        public void DeleteContact(int contactid)
        {
            using (var con = CS())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspRemoveContact", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactid;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Det gick inte att ta bort kunden");
                }
            }
        }
    }
}