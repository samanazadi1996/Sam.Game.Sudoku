using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.Wrappers;
using Sudoku.WebApi.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.WebApi.Endpoints;

public class DocEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetErrorCodes);
        builder.MapGet(GetVersion);
    }

    Dictionary<string, string> GetErrorCodes()
    {
        return Enum.GetValues<ErrorCode>().ToDictionary(t => ((int)t).ToString(), t => t.ToString());
    }

    BaseResult<string> GetVersion()
    {
        return "1.0.0";
    }
}