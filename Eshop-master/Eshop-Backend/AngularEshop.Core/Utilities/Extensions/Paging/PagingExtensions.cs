using AngularEshop.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngularEshop.Core.Utilities.Extensions.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> queryable, BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
