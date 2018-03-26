using System.Linq;
using CSCTest.DAL.EF;
using CSCTest.Data.Entities;

namespace CSCTest.Tools.DbTools
{
    public static class DbInitializer
    {
        public static void Initialize(CSCDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User { Name = "Michael", Surname = "Jackson", Email = "michael@gmail.com", Address = "Indiana", Password = "password"},
                new User { Name = "Arnold", Surname = "Schwarzenegger", Email = "arnold@gmail.com", Address = "Los Angeles", Password = "password"}
            };
            foreach (var user in users)
            {
                dbContext.Users.Add(user);
            }
            dbContext.SaveChanges();

            var organizations = new Organization[]
            {
                new Organization { Name = "Samsung", Code = "S7", Type = OrganizationType.GeneralPartnership, User = users[0]},
                new Organization { Name = "Vodafone", Code = "V8", Type = OrganizationType.IncorporatedCompany, User = users[0]},
                new Organization { Name = "T-Mobile", Code = "TMB", Type = OrganizationType.LimitedLiabilityCompany, User = users[1]},
                new Organization { Name = "AT&T branded", Code = "ATT", Type = OrganizationType.LimitedPartnership, User = users[1]},
                new Organization { Name = "XAS", Code = "XAS", Type = OrganizationType.SocialEnterprise, User = users[1]}
            };
            foreach (var organization in organizations)
            {
                dbContext.Organizations.Add(organization);
            }
            dbContext.SaveChanges();

            var countries = new Country[]
            {
                new Country { Name = "Afghanistan", Code = "AFG", Organization = organizations[0]},
                new Country { Name = "Austria", Code = "TRG", Organization = organizations[0]},
                new Country { Name = "Bulgaria", Code = "BGL", Organization = organizations[0]},
                new Country { Name = "Croatia", Code = "CRO", Organization = organizations[0]},
                new Country { Name = "Cyprus", Code = "CYY", Organization = organizations[4]},
                new Country { Name = "Czech Republic", Code = "ETL", Organization = organizations[2]},
                new Country { Name = "Greece", Code = "EUR", Organization = organizations[1]},
                new Country { Name = "Hungary", Code = "XEH", Organization = organizations[3]},
                new Country { Name = "Indonesia", Code = "XSE", Organization = organizations[4]},
                new Country { Name = "Israel", Code = "ILO", Organization = organizations[2]},
                new Country { Name = "Italy", Code = "ITV", Organization = organizations[1]},
                new Country { Name = "Libya", Code = "BTC", Organization = organizations[3]},
                new Country { Name = "Luxembourg", Code = "LUX", Organization = organizations[2]},
                new Country { Name = "Montenegro", Code = "TMT", Organization = organizations[3]}
            };
            foreach (var country in countries)
            {
                dbContext.Countries.Add(country);
            }
            dbContext.SaveChanges();

            var businessTypes = new Business[]
            {
                new Business { Name = "GIS"},
                new Business { Name = "CEO"},
                new Business { Name = "Telecommunication"},
                new Business { Name = "Managed Services"}
            };
            foreach (var businessType in businessTypes)
            {
                dbContext.Businesses.Add(businessType);
            }
            dbContext.SaveChanges();

            var familyTypes = new Family[]
            {
                new Family { Name = "Data Center", Business = businessTypes[0]},
                new Family { Name = "Cloud", Business = businessTypes[0]},
                new Family { Name = "Cyber", Business = businessTypes[1]},
                new Family { Name = "Computer Networks", Business = businessTypes[2]},
                new Family { Name = "Television Networks", Business = businessTypes[2]},
                new Family { Name = "Business-to-business", Business = businessTypes[3]},
                new Family { Name = "Marketing", Business = businessTypes[3]}
            };
            foreach (var familyType in familyTypes)
            {
                dbContext.Families.Add(familyType);
            }
            dbContext.SaveChanges();

            var offeringTypes = new Offering[]
            {
                new Offering { Name = "Data Storage", Family = familyTypes[0]},
                new Offering { Name = "Data management", Family = familyTypes[0]},
                new Offering { Name = "Biz cloud", Family = familyTypes[1]},
                new Offering { Name = "Cloud compute", Family = familyTypes[1]},
                new Offering { Name = "Consulting services", Family = familyTypes[2]},
                new Offering { Name = "Ethernet", Family = familyTypes[3]},
                new Offering { Name = "Wireless networks", Family = familyTypes[3]},
                new Offering { Name = "Cable television", Family = familyTypes[4]},
                new Offering { Name = "Satellite television", Family = familyTypes[4]},
                new Offering { Name = "Supply chain management", Family = familyTypes[5]},
                new Offering { Name = "Videoconferencing", Family = familyTypes[5]},
                new Offering { Name = "Marketing strategy", Family = familyTypes[6]},
                new Offering { Name = "Integrated advertising agency services", Family = familyTypes[6]}
            };
            foreach (var offeringType in offeringTypes)
            {
                dbContext.Offerings.Add(offeringType);
            }
            dbContext.SaveChanges();

            var countryBusinesses = new CountryBusiness[]
            {
                new CountryBusiness { Country = countries[0], Business = businessTypes[0]},
                new CountryBusiness { Country = countries[1], Business = businessTypes[0]},
                new CountryBusiness { Country = countries[1], Business = businessTypes[1]},
                new CountryBusiness { Country = countries[2], Business = businessTypes[2]},
                new CountryBusiness { Country = countries[3], Business = businessTypes[3]},
                new CountryBusiness { Country = countries[3], Business = businessTypes[1]},
                new CountryBusiness { Country = countries[4], Business = businessTypes[2]},
                new CountryBusiness { Country = countries[5], Business = businessTypes[3]},
                new CountryBusiness { Country = countries[6], Business = businessTypes[0]},
                new CountryBusiness { Country = countries[6], Business = businessTypes[2]},
                new CountryBusiness { Country = countries[6], Business = businessTypes[1]},
                new CountryBusiness { Country = countries[8], Business = businessTypes[3]},
                new CountryBusiness { Country = countries[9], Business = businessTypes[0]},
                new CountryBusiness { Country = countries[9], Business = businessTypes[3]},
                new CountryBusiness { Country = countries[10], Business = businessTypes[2]},
                new CountryBusiness { Country = countries[12], Business = businessTypes[1]},
                new CountryBusiness { Country = countries[12], Business = businessTypes[0]},
                new CountryBusiness { Country = countries[12], Business = businessTypes[2]},
                new CountryBusiness { Country = countries[13], Business = businessTypes[3]}
            };
            foreach (var countryBusiness in countryBusinesses)
            {
                dbContext.CountryBusinesses.Add(countryBusiness);
            }
            dbContext.SaveChanges();

            var businessFamilies = new BusinessFamily[]
            {
                new BusinessFamily { CountryBusiness = countryBusinesses[0], Family = familyTypes[0]},
                new BusinessFamily { CountryBusiness = countryBusinesses[0], Family = familyTypes[1]},
                new BusinessFamily { CountryBusiness = countryBusinesses[1], Family = familyTypes[1]},
                new BusinessFamily { CountryBusiness = countryBusinesses[2], Family = familyTypes[2]},
                new BusinessFamily { CountryBusiness = countryBusinesses[3], Family = familyTypes[3]},
                new BusinessFamily { CountryBusiness = countryBusinesses[4], Family = familyTypes[5]},
                new BusinessFamily { CountryBusiness = countryBusinesses[4], Family = familyTypes[6]},
                new BusinessFamily { CountryBusiness = countryBusinesses[6], Family = familyTypes[3]},
                new BusinessFamily { CountryBusiness = countryBusinesses[7], Family = familyTypes[6]},
                new BusinessFamily { CountryBusiness = countryBusinesses[8], Family = familyTypes[0]},
                new BusinessFamily { CountryBusiness = countryBusinesses[9], Family = familyTypes[3]},
                new BusinessFamily { CountryBusiness = countryBusinesses[9], Family = familyTypes[4]},
                new BusinessFamily { CountryBusiness = countryBusinesses[11], Family = familyTypes[5]},
                new BusinessFamily { CountryBusiness = countryBusinesses[12], Family = familyTypes[1]},
                new BusinessFamily { CountryBusiness = countryBusinesses[13], Family = familyTypes[6]},
                new BusinessFamily { CountryBusiness = countryBusinesses[14], Family = familyTypes[3]},
                new BusinessFamily { CountryBusiness = countryBusinesses[14], Family = familyTypes[4]},
                new BusinessFamily { CountryBusiness = countryBusinesses[16], Family = familyTypes[0]},
                new BusinessFamily { CountryBusiness = countryBusinesses[17], Family = familyTypes[4]},
                new BusinessFamily { CountryBusiness = countryBusinesses[18], Family = familyTypes[5]}
            };
            foreach (var businessFamily in businessFamilies)
            {
                dbContext.BusinessFamilies.Add(businessFamily);
            }
            dbContext.SaveChanges();

            var familyOfferings = new FamilyOffering[]
            {
                new FamilyOffering { BusinessFamily = businessFamilies[0], Offering = offeringTypes[0]},
                new FamilyOffering { BusinessFamily = businessFamilies[1], Offering = offeringTypes[2]},
                new FamilyOffering { BusinessFamily = businessFamilies[2], Offering = offeringTypes[3]},
                new FamilyOffering { BusinessFamily = businessFamilies[3], Offering = offeringTypes[4]},
                new FamilyOffering { BusinessFamily = businessFamilies[5], Offering = offeringTypes[9]},
                new FamilyOffering { BusinessFamily = businessFamilies[5], Offering = offeringTypes[10]},
                new FamilyOffering { BusinessFamily = businessFamilies[6], Offering = offeringTypes[11]},
                new FamilyOffering { BusinessFamily = businessFamilies[7], Offering = offeringTypes[5]},
                new FamilyOffering { BusinessFamily = businessFamilies[8], Offering = offeringTypes[11]},
                new FamilyOffering { BusinessFamily = businessFamilies[8], Offering = offeringTypes[12]},
                new FamilyOffering { BusinessFamily = businessFamilies[10], Offering = offeringTypes[6]},
                new FamilyOffering { BusinessFamily = businessFamilies[11], Offering = offeringTypes[7]},
                new FamilyOffering { BusinessFamily = businessFamilies[12], Offering = offeringTypes[9]},
                new FamilyOffering { BusinessFamily = businessFamilies[13], Offering = offeringTypes[2]},
                new FamilyOffering { BusinessFamily = businessFamilies[13], Offering = offeringTypes[3]},
                new FamilyOffering { BusinessFamily = businessFamilies[15], Offering = offeringTypes[5]},
                new FamilyOffering { BusinessFamily = businessFamilies[16], Offering = offeringTypes[8]},
                new FamilyOffering { BusinessFamily = businessFamilies[17], Offering = offeringTypes[1]},
                new FamilyOffering { BusinessFamily = businessFamilies[18], Offering = offeringTypes[7]},
                new FamilyOffering { BusinessFamily = businessFamilies[18], Offering = offeringTypes[8]},
                new FamilyOffering { BusinessFamily = businessFamilies[19], Offering = offeringTypes[10]}
            };
            foreach(var familyOffering in familyOfferings)
            {
                dbContext.FamilyOfferings.Add(familyOffering);
            }
            dbContext.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Departmetnt 1", FamilyOffering = familyOfferings[0]},
                new Department { Name = "Departmetnt 2", FamilyOffering = familyOfferings[1]},
                new Department { Name = "Departmetnt 3", FamilyOffering = familyOfferings[1]},
                new Department { Name = "Departmetnt 4", FamilyOffering = familyOfferings[2]},
                new Department { Name = "Departmetnt 5", FamilyOffering = familyOfferings[2]},
                new Department { Name = "Departmetnt 6", FamilyOffering = familyOfferings[2]},
                new Department { Name = "Departmetnt 7", FamilyOffering = familyOfferings[4]},
                new Department { Name = "Departmetnt 8", FamilyOffering = familyOfferings[5]},
                new Department { Name = "Departmetnt 9", FamilyOffering = familyOfferings[6]},
                new Department { Name = "Departmetnt 10", FamilyOffering = familyOfferings[7]},
                new Department { Name = "Departmetnt 11", FamilyOffering = familyOfferings[7]},
                new Department { Name = "Departmetnt 12", FamilyOffering = familyOfferings[7]},
                new Department { Name = "Departmetnt 13", FamilyOffering = familyOfferings[9]},
                new Department { Name = "Departmetnt 14", FamilyOffering = familyOfferings[10]},
                new Department { Name = "Departmetnt 15", FamilyOffering = familyOfferings[11]},
                new Department { Name = "Departmetnt 16", FamilyOffering = familyOfferings[11]},
                new Department { Name = "Departmetnt 17", FamilyOffering = familyOfferings[13]},
                new Department { Name = "Departmetnt 18", FamilyOffering = familyOfferings[14]},
                new Department { Name = "Departmetnt 19", FamilyOffering = familyOfferings[15]},
                new Department { Name = "Departmetnt 20", FamilyOffering = familyOfferings[17]},
                new Department { Name = "Departmetnt 21", FamilyOffering = familyOfferings[17]},
                new Department { Name = "Departmetnt 22", FamilyOffering = familyOfferings[17]},
                new Department { Name = "Departmetnt 23", FamilyOffering = familyOfferings[19]},
                new Department { Name = "Departmetnt 24", FamilyOffering = familyOfferings[20]},
                new Department { Name = "Departmetnt 25", FamilyOffering = familyOfferings[20]}
            };
            foreach (var department in departments)
            {
                dbContext.Departments.Add(department);
            }
            dbContext.SaveChanges();
        }   
    }
}