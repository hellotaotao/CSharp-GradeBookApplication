using System;
using System.Linq;
using GradeBook.Enums;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) throw new InvalidOperationException();
            var threshold = (int) Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
            if (grades[threshold - 1] <= averageGrade) return 'A';
            if (grades[threshold * 2 - 1] <= averageGrade) return 'B';
            if (grades[threshold * 3 - 1] <= averageGrade) return 'C';
            return grades[threshold * 4 - 1] <= averageGrade ? 'D' : 'F';
        }
    }
}