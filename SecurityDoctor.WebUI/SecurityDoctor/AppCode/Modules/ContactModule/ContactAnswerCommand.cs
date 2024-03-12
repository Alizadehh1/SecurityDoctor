using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SecurityDoctor.AppCode.Extensions;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityDoctor.AppCode.Modules.ContactModule
{
    public class ContactAnswerCommand : IRequest<Contact>
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Cannot be Empty")]
        [MinLength(3, ErrorMessage = "Cannot be less than three symbol")]
        public string AnswerMessage { get; set; }
        public class ContactAnswerCommandHandler : IRequestHandler<ContactAnswerCommand, Contact>
        {
            readonly SecurityDoctorDbContext db;
            readonly IActionContextAccessor ctx;
            public ContactAnswerCommandHandler(SecurityDoctorDbContext db,IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }

            public async Task<Contact> Handle(ContactAnswerCommand request, CancellationToken cancellationToken)
            {
                l1:
                if (!ctx.ModelIsValid())
                {
                    return new Contact
                    {
                        AnswerMessage = request.AnswerMessage,
                        Id = request.Id
                    };
                }

                var post = await db.Contacts
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (post==null)
                {
                    ctx.AddModelError("AnswerMessage", "Not Found");
                    goto l1;
                }
                else if(post.AnsweredById!=null)
                {
                    ctx.AddModelError("AnswerMessage", "Already Answered");
                }
                post.AnswerMessage = request.AnswerMessage;
                return post;
            }
        }
    }
}
