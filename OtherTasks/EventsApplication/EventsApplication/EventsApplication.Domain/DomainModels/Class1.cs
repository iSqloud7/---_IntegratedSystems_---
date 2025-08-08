using EventsApplication.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApplication.Domain.DomainModels
{
    internal class Class1
    {
    }

    /*Подолу е даден почетен код со решението од првата лабораториска вежба.

Ваша задача ќе биде постоечката MVC имплементација соодветно да ја трансформирате во Onion архтиектура.Repository слојот, вклучувајќи го интерфејсот IRepository и нгеовата генеричка имплементација Repository се вклучени во почетниот код. Дополнително, дадени се интерфејси со основните CRUD операции за сите модели. Потребно е да креирате имплементации за сите интерфејси од сервисниот слој и да ги прилагодите контролерите, така што ќе се отфрли зависноста кон ApplicationDbContext  и ќе се користат методите од сервисите.За да функционира Dependency Injection потребно е правилно да се регистрираат сервисите во Program.cs

Да се имплементира View и соодветни сервисни методи за додавање Registration. Во рамки на Index страната за Events потребно е да се додаде уште една акција Register for Event, којашто води до форма каде Attendee се избира од drop-down листа и се внесува статус на плаќање. RegistrationDate треба да се пополни автоматски со моментот на креирање на записот.

Задача за бонус поени:
Во моделот Registration да се додаде референца кон EventsApplicationUser и при креирање на запсиот да се запишува најавениот корисник

*/
}
