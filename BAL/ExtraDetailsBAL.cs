using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ExtraDetailsBAL : ExtraDetailsDAL
    {
        public bool InsertRecord(EducationDetailsBO objBO)
        {
            try
            {
                return InsertEducationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(EducationDetailsBO objBO)
        {
            try
            {
                return UpdateEducationDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllEducationDetails(EducationDetailsBO objBO)
        {
            try
            {
                return GetAllEducationDetailsByFacultyId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectEducationDetailsByID(EducationDetailsBO objbo)
        {
            try
            {
                return GetEducationDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(EducationDetailsBO objBO)
        {
            try
            {
                return Delete(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertAreaExperienceRecord(AreaExperienceBO objBO)
        {
            try
            {
                return InsertAreaExperienceDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAreaExperienceRecord(AreaExperienceBO objBO)
        {
            try
            {
                return UpdateAreaExperienceDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllAreaExperienceDetails(AreaExperienceBO objBO)
        {
            try
            {
                return GetAllAreaExperienceDetailsByFacultyId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAreaExperienceDetailsByID(AreaExperienceBO objbo)
        {
            try
            {
                return GetAreaExperienceDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAreaExperienceRecord(AreaExperienceBO objBO)
        {
            try
            {
                return DeleteAreaExperience(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertPublicationResearchRecord(PublicationResearchDetailsBO objBO)
        {
            try
            {
                return InsertPublicationResearchDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdatePublicationResearchRecord(PublicationResearchDetailsBO objBO)
        {
            try
            {
                return UpdatePublicationResearchDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllPublicationResearchDetails(PublicationResearchDetailsBO objBO)
        {
            try
            {
                return GetAllPublicationResearchByFacultyId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectPublicationResearchByID(PublicationResearchDetailsBO objbo)
        {
            try
            {
                return GetPublicationResearchByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeletePublicationResearchRecord(PublicationResearchDetailsBO objBO)
        {
            try
            {
                return DeletePublicationResearch(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertAwardsRecord(FacultyAwardsDetailsBO objBO)
        {
            try
            {
                return InsertAwardsDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAwardsRecord(FacultyAwardsDetailsBO objBO)
        {
            try
            {
                return UpdateAwardsDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllAwardsDetails(FacultyAwardsDetailsBO objBO)
        {
            try
            {
                return GetAllAwardsDetailsByFacultyId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAwardsDetailsByID(FacultyAwardsDetailsBO objbo)
        {
            try
            {
                return GetAwardsDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAwardsRecord(FacultyAwardsDetailsBO objBO)
        {
            try
            {
                return DeleteAwards(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertServiceRecord(FacultyServiceDetailsBO objBO)
        {
            try
            {
                return InsertServiceDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateServiceRecord(FacultyServiceDetailsBO objBO)
        {
            try
            {
                return UpdateServiceDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllServiceDetails(FacultyServiceDetailsBO objBO)
        {
            try
            {
                return GetAllServiceDetailsByFacultyId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectServiceDetailsByID(FacultyServiceDetailsBO objbo)
        {
            try
            {
                return GetServiceDetailsByID(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteServiceRecord(FacultyServiceDetailsBO objBO)
        {
            try
            {
                return DeleteService(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFacultyExtraDetails(int FacultyId, int LanguageId)
        {
            try
            {
                return GetFacultyExtraDetailsByFacultyId(FacultyId, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SequenceNo(int DepartmentId)
        {
            try
            {
                return SelectSequenceNo(DepartmentId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
