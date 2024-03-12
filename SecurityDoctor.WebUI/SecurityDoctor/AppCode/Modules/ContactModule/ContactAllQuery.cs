using MediatR;
using Microsoft.EntityFrameworkCore;
using SecurityDoctor.Models.DataContexts;
using SecurityDoctor.WebUI.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityDoctor.AppCode.Modules.ContactModule
{
    public class ContactAllQuery : IRequest<IEnumerable<Contact>>
    {

        public class ContactAllQueryHandler : IRequestHandler<ContactAllQuery, IEnumerable<Contact>>
        {
            readonly SecurityDoctorDbContext db;
            public ContactAllQueryHandler(SecurityDoctorDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Contact>> Handle(ContactAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Contacts
                    .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
