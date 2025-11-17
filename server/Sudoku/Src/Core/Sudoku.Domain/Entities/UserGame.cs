using Sudoku.Domain.Common;
using Sudoku.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Entities
{
    public class UserGame : AuditableBaseEntity
    {
        public UserGameStatus UserGameStatus { get; set; }
        public long Time { get; set; }
        public int Wrong { get; set; }
        public int Hint { get; set; }
        public List<List<SudokuCell>> Data { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }
    }

}
