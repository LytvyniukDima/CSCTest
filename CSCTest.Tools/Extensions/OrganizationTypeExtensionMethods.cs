using CSCTest.Data.Entities;

namespace CSCTest.Tools.Extensions
{
    public static class OrganizationTypeExtensionMethods
    {
        public static string GetStringName(this OrganizationType organizationType)
        {
            switch (organizationType)
            {
                case OrganizationType.GeneralPartnership:
                    return "General Partnership";
                case OrganizationType.LimitedPartnership:
                    return "Limited partnership";
                case OrganizationType.LimitedLiabilityCompany:
                    return "Limited Liability Company (Co. Ltd.)";
                case OrganizationType.IncorporatedCompany:
                    return "Incorporated company";
                case OrganizationType.SocialEnterprise:
                    return "Social enterprise";
                default:
                    return "Other";
            }
        }
    }
}