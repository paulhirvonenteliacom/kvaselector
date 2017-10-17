namespace kvaselector.Migrations
{
    using kvaselector.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<kvaselector.Models.kvaselectorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(kvaselector.Models.kvaselectorContext context)
        {
            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 1,
                Name = "Favorit 1"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 2,
                Name = "Favorit 2"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 3,
                Name = "Senast använda koder"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 4,
                Name = "Inga förvalda koder"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 5,
                Name = "Alla kryssrutor"
            });

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 1,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 1
            //});

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 2,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 2
            //});

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 3,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 3
            //});

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 1,
                ServiceTypeId = 1,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 2,
                ServiceTypeId = 1,
                FavoriteNameId = 5
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 3,
                ServiceTypeId = 2,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 4,
                ServiceTypeId = 2,
                FavoriteNameId = 5
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 5,
                ServiceTypeId = 3,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 6,
                ServiceTypeId = 3,
                FavoriteNameId = 5
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 1,
                Name = "Besök på mottagning",
                Description = "Öppenvårdsbesök på mottagning där en patient möter en hälso- och sjukvårdspersonal med självständigt" +
                " behandlingsansvar." + "\n" +
                "Nybesök: Öppenvårdsbesök som inte har medicinskt samband med tidigare besök eller vårdtillfälle inom samma medicinska" +
                " verksamhetsområde(klinik / basenhet / motsvarande), vårdcentral eller motsvarande." + "\n" +
                "Återbesök: Öppenvårdsbesök som har medicinskt samband med tidigare besök eller vårdtillfälle inom samma medicinska" +
                " verksamhetsområde(klinik / basenhet / motsvarande), vårdcentral eller motsvarande."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 2,
                Name = "Resurskrävande besök barn och ungdom under 18 år",
                Description = "Öppenvårdsbesök på mottagning där en patient möter en hälso- och sjukvårdspersonal med självständigt" +
               " behandlingsansvar." + "\n" +
               "Nybesök: Öppenvårdsbesök som inte har medicinskt samband med tidigare besök eller vårdtillfälle inom samma medicinska" +
               " verksamhetsområde(klinik / basenhet / motsvarande), vårdcentral eller motsvarande." + "\n" +
               "Återbesök: Öppenvårdsbesök som har medicinskt samband med tidigare besök eller vårdtillfälle inom samma medicinska" +
               " verksamhetsområde(klinik / basenhet / motsvarande), vårdcentral eller motsvarande."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 3,
                Name = "Gruppbesök",
                Description = "Öppenvårdsbesök på mottagning där fler patienter i grupp möter en hälso- och sjukvårdspersonal." +
                "\n" +
                "En grupp ska bestå av minst 2 och högst 6 patienter."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 4,
                Name = "Hembesök",
                Description = "Öppenvårdsbesök i patientens bostad eller motsvarande där patienten möter en hälso- och " +
                "sjukvårdspersonal med självständigt behandlingsansvar."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 5,
                Name = "Omfattande utredning barn och ungdom under 18 år, tal och språk",
                Description = "I utredning ingår omfattande testning, analys, kvalificerade utlåtanden och överföring av " +
                "resultat till patient/närstående. Överföring av resultat till patient och eller närstående ingår i utredningen " +
                "och rapporteras vid besök med KVÅ för avslutad utredning. Se ”Riktlinjer för Vårdval Logopedi” på Vårdgivarguiden." +
                "\n" +
                "En utredning som genomförs under en och samma dag inkl. överföring av resultat till patient och eller närstående, " +
                "rapporteras på samma besök med KVÅ för påbörjad och avslutad utredning."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 1,
                Name = "Liten lista: Diagnostisk åtgärd"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 2,
                Name = "Liten lista: Behandlande åtgärd"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 3,
                Name = "Liten lista: Utan patientbesök"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 4,
                Name = "Liten lista: Tilläggsuppgift"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 5,
                Name = "Rapportera tid med en av koderna i denna lista."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 6,
                Name = "Stor lista"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 7,
                Name = "Vid återbesök uppmärksammas framför allt nedan angivna åtgärder. De kan antingen väljas från listorna ovan eller från kryssrutorna nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 8,
                Name = "Rapporteras då företrädare representerar patienten. Åtgärden kan antingen väljas från listorna ovan eller från kryssrutan nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 9,
                Name = "Vid återbesök uppmärksammas framför allt nedan angivna åtgärder. De kan antingen väljas från listorna ovan eller från kryssrutorna nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 10,
                Name = "Ange KVÅ-kod UX001 Nybesök i förekommande fall."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 11,
                Name = "Vid varje enskilt besök ska minst en av nedanstående KVÅ-koder för utredning rapporteras. Avslutad utredning kräver " +
                        "tidigare rapporterade besök där det framgår att utredningen har påbörjats och vid behov pågår."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 12,
                Name = "Därutöver ska samtliga nedan angivna KVÅ-koder vid något tillfälle ha rapporterats under utredningen."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 13,
                Name = "Nedan angivna KVÅ-kod rapporteras då besöket även avser överföring till skola i samband med överföring till närstående."
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 1,
                Code = "F80.0A, Fonologisk språkstörning",
                Description = "Fonologisk språkstörning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 2,
                Code = "F80.0B, Utvecklingsförsening av oralmotorik",
                Description = "Utvecklingsförsening av oralmotorik"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 3,
                Code = "F80.0C, Dyslali och liknande artikulationsavvikelse",
                Description = "Dyslali och liknande artikulationsavvikelse"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 4,
                Code = "F80.1A, Grammatisk språkstörning",
                Description = "Grammatisk språkstörning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 5,
                Code = "F80.1B, Fonologisk och grammatisk språkstörning",
                Description = "Fonologisk och grammatisk språkstörning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 6,
                Code = "F80.1C, Semantisk språkstörning",
                Description = "Semantisk språkstörning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 1,
                TreatmentTypeId = 1,
                Code = "AV078, Inspelning av röst, tal och språk",
                Name = "Inspelning av röst, tal och språk",
                Description = "Dokumentation av textläsning, spontantal, uthållna vokaler m.m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 2,
                TreatmentTypeId = 1,
                Code = "AV093, Perceptuell lyssnarbedömning av röst och tal",
                Name = "Perceptuell lyssnarbedömning av röst och tal",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 3,
                TreatmentTypeId = 1,
                Code = "AV048, Videodokumentation UNS",
                Name = "Videodokumentation UNS",
                Description = "Dokumentation med rörliga bilder och eventuellt även med ljud"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 4,
                TreatmentTypeId = 1,
                Code = "AV045, Undersökning av tal",
                Name = "Undersökning av tal",
                Description = "Innefattar undersökning av ett eller flera av följande områden: andning, fonation, oralmotorik, talflyt, artikulation, prosodi och förståelighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 5,
                TreatmentTypeId = 1,
                Code = "AV108, Undersökning av språk",
                Name = "Undersökning av språk",
                Description = "Innefattar undersökning av ett eller flera av följande områden: spontantal, icke-verbal kommunikation, pragmatik, språkförståelse, auditiv förståelse, symbolförståelse, minne, fonologi, grammatik, lexikon, syntax, semantik, benämningsförmåga, repetitionsförmåga, språklig medvetenhet samt läs- och skrivförmåga"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 6,
                TreatmentTypeId = 2,
                Code = "DV089, Logopedisk behandling",
                Name = "Logopedisk behandling",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 7,
                TreatmentTypeId = 2,
                Code = "GA024, Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen",
                Name = "Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hjälpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 8,
                TreatmentTypeId = 2,
                Code = "GA025, Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol)",
                Name = "Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol)",
                Description = "Information, utprovning, förskrivning, specialanpassning, träning och uppföljning av hjälpmedel som ingår i hjälpmedelsförteckningen, (exklusive rullstol)"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 9,
                TreatmentTypeId = 2,
                Code = "DD003, Byte av röstventil",
                Name = "Byte av röstventil",
                Description = "Byte av röstprotes/röstventil utan operativ åtgärd"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 10,
                TreatmentTypeId = 2,
                Code = "GB002, Information och undervisning",
                Name = "Information och undervisning",
                Description = "Information och utbildning av väsentligt större omfattning än vad som förekommer vid ordinära kontakter, t ex skolor av olika slag, omfattande träningsmaterial etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 11,
                TreatmentTypeId = 3,
                Code = "XS003, Brevkontakt med patient",
                Name = "Brevkontakt med patient",
                Description = "Innefattar skriftlig kommunikation med patient. Brev, e-mail m. m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 12,
                TreatmentTypeId = 3,
                Code = "XS011, Telefonkontakt med anhöriga",
                Name = "Telefonkontakt med anhöriga",
                Description = "Samtalet skall journalföras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 13,
                TreatmentTypeId = 3,
                Code = "XS012, Telefonkontakt med patient",
                Name = "Telefonkontakt med patient",
                Description = "Telefon med patient. Samtalet skall journalföras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 14,
                TreatmentTypeId = 3,
                Code = "GD002, Intyg, enklare",
                Name = "Intyg, enklare",
                Description = "Till exempel sjukintyg, friskintyg, körkortsintyg, handikappersättning, intyg till skolor etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 15,
                TreatmentTypeId = 3,
                Code = "GD003, Intyg, omfattande",
                Name = "Intyg, omfattande",
                Description = "T.ex. LUH, rättsintyg, intyg för bostadsanpassning eller annat intyg av motsvarande omfattning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 16,
                TreatmentTypeId = 4,
                Code = "ZV020, Användande av tolk",
                Name = "Användande av tolk",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 17,
                TreatmentTypeId = 4,
                Code = "ZV021, Arbetsplatsbesök",
                Name = "Arbetsplatsbesök",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 18,
                TreatmentTypeId = 4,
                Code = "ZV023, Besök inom barnomsorgen och i skolan",
                Name = "Besök inom barnomsorgen och i skolan",
                Description = "Besök i förskola/skola och hos dagmamma"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 19,
                TreatmentTypeId = 4,
                Code = "ZV025, Hembesök",
                Name = "Hembesök",
                Description = "Besök i patients bostad eller där patient vistas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 20,
                TreatmentTypeId = 4,
                Code = "ZV043, Övriga besök utanför vårdenheten",
                Name = "Övriga besök utanför vårdenheten",
                Description = "saknas"
            });

            //Liten lista, TreatmentTypeId = 5, Vårdtjänst(er): Resurskrävande besök barn och ungdom under 18 år
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 21,
                TreatmentTypeId = 5,
                Code = "ZV211, 060-119 minuter (i)",
                Name = "060-119 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 22,
                TreatmentTypeId = 5,
                Code = "ZV212, 120-179 minuter (i)",
                Name = "120-179 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 23,
                TreatmentTypeId = 5,
                Code = "ZV213, 180-239 minuter (i)",
                Name = "180-239 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 24,
                TreatmentTypeId = 5,
                Code = "ZV214, 240-299 minuter (i)",
                Name = "240-299 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 25,
                TreatmentTypeId = 5,
                Code = "ZV215, 300-359 minuter (i)",
                Name = "300-359 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 26,
                TreatmentTypeId = 5,
                Code = "ZV216, 360-419 minuter (i)",
                Name = "360-419 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 27,
                TreatmentTypeId = 5,
                Code = "ZV217, 420-479 minuter (i)",
                Name = "420-479 minuter (i)",
                Description = "Tilläggskod för angivelse av tidsåtgång"
            });

            //Stor lista, TreatmentTypeId = 6
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 28,
                TreatmentTypeId = 6,
                Code = "AV078, Inspelning av röst, tal och språk",
                Name = "Inspelning av röst, tal och språk",
                Description = "Dokumentation av textläsning, spontantal, uthållna vokaler m.m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 29,
                TreatmentTypeId = 6,
                Code = "AV093, Perceptuell lyssnarbedömning av röst och tal",
                Name = "Perceptuell lyssnarbedömning av röst och tal",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 30,
                TreatmentTypeId = 6,
                Code = "AV048, Videodokumentation UNS",
                Name = "Videodokumentation UNS",
                Description = "Dokumentation med rörliga bilder och eventuellt även med ljud"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 31,
                TreatmentTypeId = 6,
                Code = "AV045, Undersökning av tal",
                Name = "Undersökning av tal",
                Description = "Innefattar undersökning av ett eller flera av följande områden: andning, fonation, oralmotorik, talflyt, artikulation, prosodi och förståelighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 32,
                TreatmentTypeId = 6,
                Code = "AV108, Undersökning av språk",
                Name = "Undersökning av språk",
                Description = "Innefattar undersökning av ett eller flera av följande områden: spontantal, icke-verbal kommunikation, pragmatik, språkförståelse, auditiv förståelse, symbolförståelse, minne, fonologi, grammatik, lexikon, syntax, semantik, benämningsförmåga, repetitionsförmåga, språklig medvetenhet samt läs- och skrivförmåga"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 33,
                TreatmentTypeId = 6,
                Code = "DV089, Logopedisk behandling",
                Name = "Logopedisk behandling",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 34,
                TreatmentTypeId = 6,
                Code = "GA024, Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen",
                Name = "Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hjälpmedel, inkl gomplatta"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 35,
                TreatmentTypeId = 6,
                Code = "GA025, Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol)",
                Name = "Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol)",
                Description = "Information, utprovning, förskrivning, specialanpassning, träning och uppföljning av hjälpmedel som ingår i hjälpmedelsförteckningen, (exklusive rullstol)"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 36,
                TreatmentTypeId = 6,
                Code = "DD003, Byte av röstventil",
                Name = "Byte av röstventil",
                Description = "Byte av röstprotes/röstventil utan operativ åtgärd"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 37,
                TreatmentTypeId = 6,
                Code = "GB002, Information och undervisning",
                Name = "Information och undervisning",
                Description = "Information och utbildning av väsentligt större omfattning än vad som förekommer vid ordinära kontakter, t ex skolor av olika slag, omfattande träningsmaterial etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 38,
                TreatmentTypeId = 6,
                Code = "XS003, Brevkontakt med patient",
                Name = "Brevkontakt med patient",
                Description = "Innefattar skriftlig kommunikation med patient. Brev, e-mail m. m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 39,
                TreatmentTypeId = 6,
                Code = "XS011, Telefonkontakt med anhöriga",
                Name = "Telefonkontakt med anhöriga",
                Description = "Samtalet skall journalföras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 40,
                TreatmentTypeId = 6,
                Code = "XS012, Telefonkontakt med patient",
                Name = "Telefonkontakt med patient",
                Description = "Telefon med patient. Samtalet skall journalföras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 41,
                TreatmentTypeId = 6,
                Code = "GD002, Intyg, enklare",
                Name = "Intyg, enklare",
                Description = "Till exempel sjukintyg, friskintyg, körkortsintyg, handikappersättning, intyg till skolor etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 42,
                TreatmentTypeId = 6,
                Code = "GD003, Intyg, omfattande",
                Name = "Intyg, omfattande",
                Description = "T.ex. LUH, rättsintyg, intyg för bostadsanpassning eller annat intyg av motsvarande omfattning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 43,
                TreatmentTypeId = 6,
                Code = "ZV020, Användande av tolk",
                Name = "Användande av tolk",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 44,
                TreatmentTypeId = 6,
                Code = "ZV021, Arbetsplatsbesök",
                Name = "Arbetsplatsbesök",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 45,
                TreatmentTypeId = 6,
                Code = "ZV023, Besök inom barnomsorgen och i skolan",
                Name = "Besök inom barnomsorgen och i skolan",
                Description = "Besök i förskola/skola och hos dagmamma"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 46,
                TreatmentTypeId = 6,
                Code = "ZV025, Hembesök",
                Name = "Hembesök",
                Description = "Besök i patients bostad eller där patient vistas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 47,
                TreatmentTypeId = 6,
                Code = "ZV043, Övriga besök utanför vårdenheten",
                Name = "Övriga besök utanför vårdenheten",
                Description = "saknas"
            });

            //Krysslista, TreatmentTypeId = 7. Vårdtjänst(er): Besök på mottagning
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 48,
                TreatmentTypeId = 7,
                Code = "AU120, Upprättande av strukturerad vård- och omsorgsplan (i)",
                Name = "Upprättande av strukturerad vård- och omsorgsplan (i)",
                Description = "Upprättande/reviderande av skriftlig strukturerad vård- och omsorgsplan Planen ska om möjligt utformas " +
                                "tillsammans med berörd individ. I planen ska beskrivas planerad och beslutad vård och omsorg. För " +
                                "åtgärderna i planen ska anges mål. Planen ska utvärderas och omprövas. Den ska dokumenteras och det " +
                                "ska finnas en ansvarig person för att planen tas fram och justeras."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 49,
                TreatmentTypeId = 7,
                Code = "GA024, Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen (i)",
                Name = "Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen (i)",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hjälpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 50,
                TreatmentTypeId = 7,
                Code = "GA025, Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol) (i)",
                Name = "Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol) (i)",
                Description = "Information, utprovning, förskrivning, specialanpassning, träning och uppföljning av hjälpmedel som ingår" +
                                " i hjälpmedelsförteckningen, (exklusive rullstol)."
            });

            //Krysslista, TreatmentTypeId = 8. Vårdtjänst(er): Besök på mottagning
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 51,
                TreatmentTypeId = 8,
                Code = "XS001, Information och rådgivning med företrädare för patienten (i)",
                Name = "Information och rådgivning med företrädare för patienten (i)",
                Description = "Möte där någon företrädare för patienten (vårdnadshavare, god man, " +
                                "anhörig, närstående eller annan som patienten själv utsett som företrädare)" +
                                " möter hälso- och sjukvårdspersonal utan att patienten är med. Samtalet ska" +
                                " journalföras."
            });

            //Krysslista, TreatmentTypeId = 9. Vårdtjänst(er): Resurskrävande besök barn och ungdom under 18 år
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 52,
                TreatmentTypeId = 9,
                Code = "AE020, Undersökning av oralmotorik (i)",
                Name = "Undersökning av oralmotorik (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: orofacial anatomi, motorik och sensorik."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 53,
                TreatmentTypeId = 9,
                Code = "AU120, Upprättande av strukturerad vård- och omsorgsplan (i)",
                Name = "Upprättande av strukturerad vård- och omsorgsplan (i)",
                Description = "Upprättande/reviderande av skriftlig strukturerad vård- och omsorgsplan Planen ska om möjligt utformas " +
                                "tillsammans med berörd individ. I planen ska beskrivas planerad och beslutad vård och omsorg. För åtgärderna " +
                                "i planen ska anges mål. Planen ska utvärderas och omprövas. Den ska dokumenteras och det ska finnas en ansvarig " +
                                "person för att planen tas fram och justeras."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 54,
                TreatmentTypeId = 9,
                Code = "AV045, Undersökning av tal (i)",
                Name = "Undersökning av tal (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: andning, fonation, oralmotorik, " +
                                "talflyt, artikulation, prosodi och förståelighet."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 55,
                TreatmentTypeId = 9,
                Code = "AV105, Undersökning av kommunikation (i)",
                Name = "Undersökning av kommunikation (i)",
                Description = "Förståelse och hantering av språkets övergripande kontext. Förmåga till samspel t ex turtagning, " +
                                "initiativ och respons i samtal."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 56,
                TreatmentTypeId = 9,
                Code = "AV108, Undersökning av språk (i)",
                Name = "Undersökning av språk (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: spontantal, icke-verbal kommunikation, " +
                                "pragmatik, språkförståelse, auditiv förståelse, symbolförståelse, minne, fonologi, grammatik, lexikon, " +
                                "syntax, semantik, benämningsförmåga, repetitionsförmåga, språklig medvetenhet samt läs- och skrivförmåga."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 57,
                TreatmentTypeId = 9,
                Code = "AV111, Undersökning av sväljning, ät- och drickförmåga (i)",
                Name = "Undersökning av sväljning, ät- och drickförmåga (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: andning, röst, orofacial anatomi, " +
                                "motorik och sensorik, oral och faryngeal sväljningsfas samt kompensatoriska åtgärder."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 58,
                TreatmentTypeId = 9,
                Code = "GA024, Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen (i)",
                Name = "Utprovning och förskrivning av utrustning som ej ingår i hjälpmedelsförteckningen (i)",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hjälpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 59,
                TreatmentTypeId = 9,
                Code = "GA025, Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol) (i)",
                Name = "Utprovning och förskrivning av övriga hjälpmedel (exklusive rullstol) (i)",
                Description = "Information, utprovning, förskrivning, specialanpassning, träning och uppföljning av hjälpmedel " +
                                "som ingår i hjälpmedelsförteckningen, (exklusive rullstol)."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 60,
                TreatmentTypeId = 9,
                Code = "GB009, Information och undervisning riktad till patient (i)",
                Name = "Information och undervisning riktad till patient (i)",
                Description = "Information till och utbildning av patient av väsentligt större omfattning än vad som förekommer " +
                                "vid ordinära kontakter, t ex smärtskola, astmaskola, KOL-skola, ryggskola eller strukturerade " +
                                "patientutbildningar av olika slag."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 61,
                TreatmentTypeId = 9,
                Code = "GB010, Information och undervisning riktad till närstående (i)",
                Name = "Information och undervisning riktad till närstående (i)",
                Description = "Information till och utbildning av närstående av väsentligt större omfattning än vad som förekommer " +
                                "vid ordinära kontakter, t ex smärtskola, astmaskola, KOL-skola, ryggskola eller strukturerade " +
                                "närståendeutbildningar av olika slag."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 62,
                TreatmentTypeId = 10,
                Code = "UX001, Nybesök",
                Name = "Nybesök",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 63,
                TreatmentTypeId = 11,
                Code = "UV006, Tal- och språkutredning, påbörjad",
                Name = "Tal- och språkutredning, påbörjad",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 64,
                TreatmentTypeId = 11,
                Code = "UV007, Tal- och språkutredning, pågående",
                Name = "Tal- och språkutredning, pågående",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 65,
                TreatmentTypeId = 11,
                Code = "UV008, Tal- och språkutredning, avslutad",
                Name = "Tal- och språkutredning, avslutad",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 66,
                TreatmentTypeId = 12,
                Code = "AV045, Undersökning av tal (i)",
                Name = "Undersökning av tal (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: andning, fonation, oralmotorik, " +
                                "talflyt, artikulation, prosodi och förståelighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 67,
                TreatmentTypeId = 12,
                Code = "AV108, Undersökning av språk (i)",
                Name = "Undersökning av språk (i)",
                Description = "Innefattar undersökning av ett eller flera av följande områden: spontantal, icke-verbal kommunikation, " +
                                "pragmatik, språkförståelse, auditiv förståelse, symbolförståelse, minne, fonologi, grammatik, lexikon, " +
                                "syntax, semantik, benämningsförmåga, repetitionsförmåga, språklig medvetenhet samt läs- och skrivförmåga."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 68,
                TreatmentTypeId = 12,
                Code = "GD003, Intyg omfattande (i)",
                Name = "Intyg omfattande (i)",
                Description = "T.ex. LUH, rättsintyg, intyg för bostadsanpassning eller annat intyg av motsvarande omfattning."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 69,
                TreatmentTypeId = 13,
                Code = "GB004, Patientrelaterad extern information och utbildning (i)",
                Name = "Patientrelaterad extern information och utbildning (i)",
                Description = "Avser dels överrapportering av patient till annan enhet och dels information och/eller " +
                                "utbildning avseende en viss patients behov till externa enheter (=utanför den egna kliniken)."
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 1,
                ServiceType = 1,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KVÅ-koder inte obligatoriska för att få ersättning. Vårdgivaren rapporterar lämpliga KVÅ-koder där så är möjligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 2,
                CheckedTreatmentTypeId = "7,8"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 2,
                ServiceType = 2,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KVÅ-koder inte obligatoriska för att få ersättning. Vårdgivaren rapporterar lämpliga KVÅ-koder där så är möjligt.",
                NoOfTreatmentCodes = 9,
                TreatmentTypeId = "5,1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 2,
                CheckedTreatmentTypeId = "9,8"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 3,
                ServiceType = 3,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KVÅ-koder inte obligatoriska för att få ersättning. Vårdgivaren rapporterar lämpliga KVÅ-koder där så är möjligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 1,
                CheckedTreatmentTypeId = "10"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 4,
                ServiceType = 4,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KVÅ-koder inte obligatoriska för att få ersättning. Vårdgivaren rapporterar lämpliga KVÅ-koder där så är möjligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 1,
                CheckedTreatmentTypeId = "10"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 5,
                ServiceType = 5,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 3,
                CheckedTreatmentTypeId = "11,12,13"
            });

            context.SaveChanges();
        }
    }
}