using Sneakers.DTO.ResponseModel.Inner;
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

        public string Sneakers(bool isCount, bool isExport, int limit, int skip, SNEAKER_FILTER_VIEW_MODEL model)
        {
            string result = "";
            string count = @"select COUNT(*) as totalCount";
            string mainStart = String.Format(
                            @" 	DECLARE @Skip_val INT 
	                            SET @Skip_val = {0}
	                            DECLARE @Limit_val INT 
	                            SET @Limit_val = {1}
	                            DECLARE @Brand INT 
	                            SET @Brand = {2}
	                            DECLARE @SneakersModelId INT
	                            SET @SneakersModelId = {3}
	                            DECLARE @SneakersTypeId INT
	                            SET @SneakersTypeId = {4}
                                 DECLARE @Price INT 
	                            SET @Price = {5}",

                            skip,
                            limit,
                            model.BrandId,
                            model.ModelId,
                            model.TypeId,
                            model.Price
            );
            string variables = @" SELECT 
							snk.ID,
                            brand.BRAND 'BRAND',
                            model.MODEL 'MODEL',
                            type.TYPE 'TYPE',
                            snk.PRICE ";

            string cases = " ";
            filterSneakers(ref cases, model);


            string mainPart = @"
	                from SNEAKERS as snk
                    left join SNEAKERS_BRAND as brand
                    on brand.ID = snk.BRAND_ID
                    left join SNEAKERS_MODEL as model
                    on model.ID = snk.MODEL_ID
                    left join SNEAKERS_TYPE as type
                    on TYPE.ID = snk.TYPE_ID";

            string order = " ORDER BY snk.ID DESC  ";

            string end = @"OFFSET @skip_val ROWS FETCH NEXT @Limit_val ROWS ONLY";

            if (!isCount && !isExport)
            {
                result = mainStart + variables + mainPart + cases + order + end;
            }
            else if (isCount && !isExport)
            {
                result = mainStart + count + mainPart + cases;
            }
            else if (!isCount && isExport)
            {
                result = mainStart + variables + mainPart + cases + order;
            }
            return result;
        }

        public void filterSneakers(ref string cases, SNEAKER_FILTER_VIEW_MODEL model)
        {

           

            if (model.BrandId != 0)
            {
                cases += " and ";
                cases += " snk.BRAND_ID = @Brand ";
            }
            if (model.TypeId != 0)
            {
                cases += " and ";
                cases += " snk.TYPE_ID = @SneakersTypeId ";
            }
            if (model.ModelId != 0)
            {
                cases += " and ";
                cases += " snk.MODEL_ID = @SneakersModelId ";
            }
        }

    }
}
