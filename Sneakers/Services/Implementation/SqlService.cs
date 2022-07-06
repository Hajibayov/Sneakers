using Sneakers.Services.Interface;
using System;

namespace Sneakers.Services.Implementation
{
    public class SqlService:ISqlService
    {
        public string Brands(bool isCount, bool isExport, int limit, int skip)
        {
            string result = "";

            string count = @" SELECT COUNT(*) as 'totalCount' ";

            string mainStart = String.Format(
                            @"DECLARE @Skip_val int
							SET @Skip_val = {0}
							DECLARE @Limit_val int
							SET @Limit_val = {1}
                            ",
                            skip,
                            limit);

            string variables = @" SELECT
                            brd.ID as 'Id',
                            brd.BRAND as 'Brand'";

            string mainPart = @" FROM
							SNEAKERS_BRAND as brd ";

            string order = " ORDER BY brd.BRAND ASC  ";
            string end = @" OFFSET @skip_val ROWS FETCH NEXT @Limit_val ROWS ONLY ";

            if (!isCount && !isExport)
            {
                result = mainStart + variables + mainPart + order + end;
            }
            else if (isCount && !isExport)
            {
                result = mainStart + count + mainPart;
            }
            else if (!isCount && isExport)
            {
                result = mainStart + variables + mainPart + order;
            }

            return result;
        }

        public string Types(bool isCount, bool isExport, int limit, int skip)
        {
            string result = "";

            string count = @" SELECT COUNT(*) as 'totalCount' ";

            string mainStart = String.Format(
                            @"DECLARE @Skip_val int
							SET @Skip_val = {0}
							DECLARE @Limit_val int
							SET @Limit_val = {1}
                            ",
                            skip,
                            limit);

            string variables = @" SELECT
                            typ.ID as 'Id',
                            typ.TYPE as 'Type'";

            string mainPart = @" FROM
							SNEAKERS_TYPE as typ ";

            string order = " ORDER BY typ.TYPE ASC  ";
            string end = @" OFFSET @skip_val ROWS FETCH NEXT @Limit_val ROWS ONLY ";

            if (!isCount && !isExport)
            {
                result = mainStart + variables + mainPart + order + end;
            }
            else if (isCount && !isExport)
            {
                result = mainStart + count + mainPart;
            }
            else if (!isCount && isExport)
            {
                result = mainStart + variables + mainPart + order;
            }

            return result;
        }

        public string Sizes(bool isCount, bool isExport, int limit, int skip)
        {
            string result = "";

            string count = @" SELECT COUNT(*) as 'totalCount' ";

            string mainStart = String.Format(
                            @"DECLARE @Skip_val int
							SET @Skip_val = {0}
							DECLARE @Limit_val int
							SET @Limit_val = {1}
                            ",
                            skip,
                            limit);

            string variables = @" SELECT
                            siz.ID as 'Id',
                            siz.SIZE as 'Size'";

            string mainPart = @" FROM
							SIZE as siz ";

            string order = " ORDER BY siz.SIZE ASC  ";
            string end = @" OFFSET @skip_val ROWS FETCH NEXT @Limit_val ROWS ONLY ";

            if (!isCount && !isExport)
            {
                result = mainStart + variables + mainPart + order + end;
            }
            else if (isCount && !isExport)
            {
                result = mainStart + count + mainPart;
            }
            else if (!isCount && isExport)
            {
                result = mainStart + variables + mainPart + order;
            }

            return result;
        }
        public string Models(bool isCount, bool isExport, int limit, int skip)
        {
            string result = "";

            string count = @" SELECT COUNT(*) as 'totalCount' ";

            string mainStart = String.Format(
                            @"DECLARE @Skip_val int
							SET @Skip_val = {0}
							DECLARE @Limit_val int
							SET @Limit_val = {1}
                            ",
                            skip,
                            limit);

            string variables = @" SELECT
                            mdl.ID as 'Id',
                            mdl.MODEL as 'Model'";

            string mainPart = @" FROM
							SNEAKERS_MODEL as mdl ";

            string order = " ORDER BY mdl.MODEL ASC  ";
            string end = @" OFFSET @skip_val ROWS FETCH NEXT @Limit_val ROWS ONLY ";

            if (!isCount && !isExport)
            {
                result = mainStart + variables + mainPart + order + end;
            }
            else if (isCount && !isExport)
            {
                result = mainStart + count + mainPart;
            }
            else if (!isCount && isExport)
            {
                result = mainStart + variables + mainPart + order;
            }

            return result;
        }

    }
}
