using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryCommentCommand:IRequest<Guid>
{
    public Guid? EntryId { get; set; }
    public Guid? CreatedById { get; set; }
    public string Content { get; set; }

    public CreateEntryCommentCommand(Guid? entryId, Guid? createdBy, string content)
    {
        EntryId = entryId;
        CreatedById = createdBy;
        Content = content;
    }
}
