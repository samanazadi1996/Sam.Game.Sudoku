using Microsoft.EntityFrameworkCore;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Wrappers;
using Sudoku.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku.Application.Features.UserGames.Commands.CreateGame;

public class CreateGameCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : IRequestHandler<CreateGameCommand, BaseResult>
{
    public async Task<BaseResult> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var userId = authenticatedUser.GetUserId();

        var lastGames = await unitOfWork.UserGames.Get().Where(p => p.UserId == userId)
                .Where(p => p.UserGameStatus == Domain.Enums.UserGameStatus.Active)
                .Include(p => p.Game)
                .ToListAsync();

        foreach (var item in lastGames)
            item.UserGameStatus = Domain.Enums.UserGameStatus.Inactive;

        var game = await unitOfWork.Games.Get()
            .Where(p => p.Level == request.GameLevel)
            .Include(p => p.UserGames)
            .Where(p => !p.UserGames.Any(x => x.GameId == p.Id))
            .FirstOrDefaultAsync();
        foreach (var item in game.Data)
        {
            foreach (var item1 in item)
            {
                if (item1.Status == Domain.Enums.SudokuCellStatus.Empty)
                {
                    item1.Number = null;
                }
                item1.Note = [];
            }

        }

        var entity = new UserGame()
        {
            GameId = game.Id,
            UserId = userId,
            Data = game.Data,
            Hint = 0,
            Time = 0,
            Wrong = 0,
            UserGameStatus = Domain.Enums.UserGameStatus.Active
        };
        await unitOfWork.UserGames.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}