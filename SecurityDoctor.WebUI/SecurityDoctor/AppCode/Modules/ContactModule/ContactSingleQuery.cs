using MediatR;
using Microsoft.EntityFrameworkCore;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.WebUI.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityDoctor.AppCode.Modules.ContactModule
{
    public class ContactSingleQuery : IRequest<Contact>
    {
        public int Id { get; set; }
        public class ContactSingleQueryHandler : IRequestHandler<ContactSingleQuery, Contact>
        {
            readonly SecurityDoctorDbContext db;
            public ContactSingleQueryHandler(SecurityDoctorDbContext db)
            {
                this.db = db;
            }
            public async Task<Contact> Handle(ContactSingleQuery request, CancellationToken cancellationToken)
            {

                var model = await db.Contacts
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

                return model;
            }
        }
    }
}
