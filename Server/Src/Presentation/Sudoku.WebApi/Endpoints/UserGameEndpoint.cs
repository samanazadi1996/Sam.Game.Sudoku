using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Sudoku.Application.Features.UserGames.Commands.ClearColumn;
using Sudoku.Application.Features.UserGames.Commands.CreateGame;
using Sudoku.Application.Features.UserGames.Commands.WriteNote;
using Sudoku.Application.Features.UserGames.Commands.WriteSudokuCell;
using Sudoku.Application.Features.UserGames.Queries.GetActiveUserGame;
using Sudoku.Application.Features.UserGames.Queries.HasSavedGame;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using Sudoku.WebApi.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace Sudoku.WebApi.Endpoints;

public class UserGameEndpoint : EndpointGroupBase
{
    public override void Map(RouteGroupBuilder builder)
    {
        builder.MapGet(GetActiveUserGame).RequireAuthorization();

        builder.MapGet(HasSavedGame).RequireAuthorization();

        builder.MapPost(CreateGame).RequireAuthorization();

        builder.MapPost(WriteSudokuCell).RequireAuthorization();

        builder.MapPost(ClearColumn).RequireAuthorization();

        builder.MapPost(WriteNote).RequireAuthorization();

    }

    async Task<BaseResult<GetActiveUserGameResponse>> GetActiveUserGame(IMediator mediator)
        => await mediator.Send<GetActiveUserGameQuery, BaseResult<GetActiveUserGameResponse>>(new GetActiveUserGameQuery());

    async Task<BaseResult<HasSavedGameResponse>> HasSavedGame(IMediator mediator)
        => await mediator.Send<HasSavedGameQuery, BaseResult<HasSavedGameResponse>>(new HasSavedGameQuery());

    async Task<BaseResult> CreateGame(IMediator mediator, CreateGameCommand request)
        => await mediator.Send<CreateGameCommand, BaseResult>(request);

    async Task<BaseResult<SudokuCell>> WriteSudokuCell(IMediator mediator, WriteSudokuCellCommand request)
        => await mediator.Send<WriteSudokuCellCommand, BaseResult<SudokuCell>>(request);

    async Task<BaseResult<SudokuCell>> ClearColumn(IMediator mediator, ClearColumnCommand request)
        => await mediator.Send<ClearColumnCommand, BaseResult<SudokuCell>>(request);

    async Task<BaseResult<SudokuCell>> WriteNote(IMediator mediator, WriteNoteCommand request)
        => await mediator.Send<WriteNoteCommand, BaseResult<SudokuCell>>(request);

}
