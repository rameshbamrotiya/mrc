using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Interface.Candidate
{
    public interface ICandidateDetailsRepository : IDisposable
    {
        List<CandidateDetailsModel> GetAllTblCandidateDetails();
        List<CandidateEducationDetailsModel> GetAllTblCandidateEducationDetails();
        List<GetAllCandidateRecruitmentMasterResult> GetAllCandidateRecruitmentMaster();
        List<CandidateExperienceDetailsModel> GetAllTblCandidateExperienceDetails();
        List<CandidateCourseDetailsModel> GetAllTblCandidateCourseDetails();
        List<CandidateFamilyDetailsModel> GetAllTblCandidateFamilyDetails();
        List<CandidateProfessionalReferralDetailsModel> GetAllTblCandidateProfessionalReferralDetails();
        List<CandidateIndexedDetailsModels> GetAllCandidateIndexedJournalDetails();
        List<CandidateNonIndexedDetailsModels> GetAllCandidateNonIndexedJournalDetails();

        CandidateDetailsModel GetTblCandidateDetailsById(long lgId);
        CandidateEducationDetailsModel GetTblCandidateEducationDetailsById(long lgId);
        CandidateExperienceDetailsModel GetTblCandidateExperienceDetailsById(long lgId);
        CandidateCourseDetailsModel GetTblCandidateCourseDetailsById(long lgId);
        CandidateFamilyDetailsModel GetTblCandidateFamilyDetailsById(long lgId);
        CandidateProfessionalReferralDetailsModel GetTblCandidateProfessionalReferralDetailsById(long lgId);
        CandidateIndexedDetailsModels GetCandidateIndexedJournalDetailsById(long lgId);
        CandidateNonIndexedDetailsModels GetCandidateNonIndexedJournalDetailsById(long lgId);
        GetCandidateExperienceTotalModel GetTblCandidateExperienceTotalDetailsByCandId(long lgId);
        CandidateDetailsForExperienceModel GetCandidateDetailsForExtraExperienceByCanId(long lgId);
        CandidateDetailsForTeachingPostsModel GetCandidateDetailsForExtraTeachingPostsByCanId(long lgId);
        CandidateDetailsForAcademicModel GetCandidateDetailsForAcademicByCanId(long lgId);

        List<CandidateEducationDetailsModel> GetAllCandidateEducationDetailsByCandId(long lgId);
        List<CandidateExperienceDetailsModel> GetAllCandidateExperienceDetailsByCandId(long lgId);
        List<CandidateCourseDetailsModel> GetAllCandidateCourseDetailsByCandId(long lgId);
        List<CandidateFamilyDetailsModel> GetAllCandidateFamilyDetailsByCandId(long lgId);
        List<CandidateProfessionalReferralDetailsModel> GetAllCandidateProfessionalReferralDetailsByCandId(long lgId);
        List<CandidateIndexedDetailsModels> GetAllCandidateIndexedJournalDetailsByCandId(long lgId);
        List<CandidateNonIndexedDetailsModels> GetAllCandidateNonIndexedJournalDetailsByCandId(long lgId);

        bool InsertOrUpdateTblCandidateDetails(CandidateDetailsModel objData, out string strError);
        bool InsertOrUpdateTblCandidateEducationDetails(CandidateEducationDetailsModel objData, out string strError);
        bool InsertOrUpdateTblCandidateExperienceDetails(CandidateExperienceDetailsModel objData, out string strError);
        bool InsertOrUpdateTblCandidateCourseDetails(CandidateCourseDetailsModel objData, out string strError);
        bool InsertOrUpdateTblCandidateFamilyDetails(CandidateFamilyDetailsModel objData, out string strError);
        bool InsertOrUpdateTblCandidateProfessionalReferralDetails(CandidateProfessionalReferralDetailsModel objData, out string strError);
        bool InsertOrUpdateCandidateIndexedJournal(CandidateIndexedDetailsModels objData, out string strError);
        bool InsertOrUpdateCandidateNonIndexedJournal(CandidateNonIndexedDetailsModels objData, out string strError);
        bool UpdateCandidateDetailsForExperience(CandidateDetailsForExperienceModel objData, out string strError);
        bool UpdateCandidateDetailsForTeachingPosts(CandidateDetailsForTeachingPostsModel objData, out string strError);
        bool UpdateCandidateDetailsForAcademic(CandidateDetailsForAcademicModel objData, out string strError);
        bool UnlockProfileForEdit(long candId, long addId, out string strError);

        bool RemoveTblCandidateDetails(long lgId, out string strError);
        bool RemoveTblCandidateEducationDetails(long lgId, out string strError);
        bool RemoveTblCandidateExperienceDetails(long lgId, out string strError);
        bool RemoveTblCandidateCourseDetails(long lgId, out string strError);
        bool RemoveTblCandidateFamilyDetails(long lgId, out string strError);
        bool RemoveTblCandidateProfessionalReferralDetails(long lgId, out string strError);
        bool UpdateCandidateDetailsExtraInfo(CandidateDetailsModel objdata, out string strError);
        bool RemoveTblCandidateIndexedJournalDetails(long lgId, out string strError);
        bool RemoveTblCandidateNonIndexedJournalDetails(long lgId, out string strError);
    }
}
