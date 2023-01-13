using System.Data;

namespace Grading_System.Models
{
    public interface IGradesWord
    {
        DataTable ExportWord(int studentId);
    }
}