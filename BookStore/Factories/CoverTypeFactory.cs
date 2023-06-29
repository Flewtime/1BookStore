using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class CoverTypeFactory
    {
        public static CoverType createCoverType(string CoverTypeName, string CoverTypeMaterial)
        {
            CoverType coverType = new CoverType();
            coverType.CoverTypeName = CoverTypeName;
            coverType.CoverTypeMaterial = CoverTypeMaterial;
            return coverType;
        }
    }
}