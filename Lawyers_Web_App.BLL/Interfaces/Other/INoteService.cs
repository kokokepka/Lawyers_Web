using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Other
{
    public interface INoteService
    {
        void MakeNote(NoteDTO noteDto);
        NoteDTO GetNote(int? id);
        IEnumerable<NoteDTO> GetUserNotes(UserDTO userDto);
        void Dispose();
    }
}
