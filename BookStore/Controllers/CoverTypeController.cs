using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using System.Text.RegularExpressions;
using BookStore.Views;

namespace BookStore.Controllers
{
    public class CoverTypeController
    {
        private CoverTypeHandler coverTypeHandler;

        public CoverTypeController()
        {
            coverTypeHandler = new CoverTypeHandler();
        }

        public string insertCoverType(string CoverTypeName, string CoverTypeMaterial, Boolean checkCoverTypeName)
        {
            string validate = validateCoverType(CoverTypeName, CoverTypeMaterial, checkCoverTypeName);
            if(validate.Equals("Insert Success!"))
            {
                coverTypeHandler.insertCoverType(CoverTypeName, CoverTypeMaterial);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteCoverType(int CoverTypeID, string Path)
        {
            coverTypeHandler.deleteCoverType(CoverTypeID, Path);
        }

        public string updateCoverType(int CoverTypeID, string CoverTypeName, string CoverTypeMaterial, Boolean checkCoverTypeName)
        {
            string validate = validateCoverType(CoverTypeName, CoverTypeMaterial, checkCoverTypeName);
            if(validate.Equals("Insert Success!"))
            {
                coverTypeHandler.updateCoverType(CoverTypeID, CoverTypeName, CoverTypeMaterial);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public CoverType findCoverTypeByID(int CoverTypeID)
        {
            return coverTypeHandler.findCoverTypeByID(CoverTypeID);
        }

        public List<CoverType> getAllCoverType()
        {
            return coverTypeHandler.getAllCoverType();
        }

        private string validateCoverType(string CoverTypeName, string CoverTypeMaterial, Boolean checkCoverTypeName)
        {
            Regex validateCoverTypeName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");
            Regex validateCoverTypeMaterial = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");

            Boolean isCoverTypeNameExist = false;
            List<CoverType> coverTypeList = getAllCoverType();
            foreach (CoverType coverType in coverTypeList)
            {
                if (coverType.CoverTypeName.Equals(CoverTypeName))
                {
                    isCoverTypeNameExist = true;
                    break;
                }
            }

            if (CoverTypeName == "" || CoverTypeMaterial == "")
            {
                return "Please Fill All The Fields!";
            }
            else if(CoverTypeName.Length < 2)
            {
                return "Cover Type Name Must be More Than 2 Characters!";
            }
            else if (!validateCoverTypeName.IsMatch(CoverTypeName))
            {
                return "Please Enter A Valid Cover Type Name!";
            }
            else if(checkCoverTypeName && isCoverTypeNameExist)
            {
                return "Cover Type Name Already Exist, Please Enter Another Cover Type Name!";
            }
            else if(CoverTypeMaterial.Length < 2)
            {
                return "Cover Type Material Must be More Than 2 Characters!";
            }
            else if (!validateCoverTypeMaterial.IsMatch(CoverTypeMaterial))
            {
                return "Please Enter A Valid Cover Type Material!";
            }

            return "Insert Success!";
        }
    }
}