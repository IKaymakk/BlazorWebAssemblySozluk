using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.RequestModels;

public class CreateEntryCommentVoteCommand:IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType voteType { get; set; }

    public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid createdBy, VoteType voteType)
    {
        EntryCommentId = entryCommentId;
        CreatedBy = createdBy;
        this.voteType = voteType;
    }
}
