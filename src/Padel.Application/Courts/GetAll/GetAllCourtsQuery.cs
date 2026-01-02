using Padel.API.Courts.GetAll;
using Padel.Application.Shared.Messaging;
using Padel.Application.Shared.PaginationAndSorting;

namespace Padel.Application.Courts.GetAll;

public sealed record GetAllCourtsQuery(
    PaginationParameters Pagination,
    SortingParameters Sorting
) : IQuery<PagedResult<GetAllCourtsItem>>;
