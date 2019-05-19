﻿using BenchmarkDotNet.Attributes;
using Runner.Setup;
using System.Linq;
using Unity;

namespace Performance.Tests
{
    [BenchmarkCategory("Registration")]
    [Config(typeof(BenchmarkConfiguration))]
    public class RegistrationBase
    {
        IUnityContainer _container = new UnityContainer();

        readonly Foo00 instance00 = new Foo00();
        readonly Foo01 instance01 = new Foo01();
        readonly Foo02 instance02 = new Foo02();
        readonly Foo03 instance03 = new Foo03();
        readonly Foo04 instance04 = new Foo04();
        readonly Foo05 instance05 = new Foo05();
        readonly Foo06 instance06 = new Foo06();
        readonly Foo07 instance07 = new Foo07();
        readonly Foo08 instance08 = new Foo08();
        readonly Foo09 instance09 = new Foo09();
        readonly Foo10 instance10 = new Foo10();
        readonly Foo11 instance11 = new Foo11();
        readonly Foo12 instance12 = new Foo12();
        readonly Foo13 instance13 = new Foo13();
        readonly Foo14 instance14 = new Foo14();
        readonly Foo15 instance15 = new Foo15();
        readonly Foo16 instance16 = new Foo16();
        readonly Foo17 instance17 = new Foo17();
        readonly Foo18 instance18 = new Foo18();
        readonly Foo19 instance19 = new Foo19();
        protected object[] _storage = new object[20];


        [IterationSetup]
        public virtual void SetupContainer()
        {
            _container.RegisterType<PocoWithDependency00>();
            _container.RegisterType<PocoWithDependency01>();
            _container.RegisterType<PocoWithDependency02>();
            _container.RegisterType<PocoWithDependency03>();
            _container.RegisterType<PocoWithDependency04>();
            _container.RegisterType<PocoWithDependency05>();
            _container.RegisterType<PocoWithDependency06>();
            _container.RegisterType<PocoWithDependency07>();
            _container.RegisterType<PocoWithDependency08>();
            _container.RegisterType<PocoWithDependency09>();
            _container.RegisterType<PocoWithDependency10>();
            _container.RegisterType<PocoWithDependency11>();
            _container.RegisterType<PocoWithDependency12>();
            _container.RegisterType<PocoWithDependency13>();
            _container.RegisterType<PocoWithDependency14>();
            _container.RegisterType<PocoWithDependency15>();
            _container.RegisterType<PocoWithDependency16>();
            _container.RegisterType<PocoWithDependency17>();
            _container.RegisterType<PocoWithDependency18>();
            _container.RegisterType<PocoWithDependency19>();

            _container.RegisterType<IFoo00, Foo00>();
            _container.RegisterType<IFoo01, Foo01>();
            _container.RegisterType<IFoo02, Foo02>();
            _container.RegisterType<IFoo03, Foo03>();
            _container.RegisterType<IFoo04, Foo04>();
            _container.RegisterType<IFoo05, Foo05>();
            _container.RegisterType<IFoo06, Foo06>();
            _container.RegisterType<IFoo07, Foo07>();
            _container.RegisterType<IFoo08, Foo08>();
            _container.RegisterType<IFoo09, Foo09>();
            _container.RegisterType<IFoo10, Foo10>();
            _container.RegisterType<IFoo11, Foo11>();
            _container.RegisterType<IFoo12, Foo12>();
            _container.RegisterType<IFoo13, Foo13>();
            _container.RegisterType<IFoo14, Foo14>();
            _container.RegisterType<IFoo15, Foo15>();
            _container.RegisterType<IFoo16, Foo16>();
            _container.RegisterType<IFoo17, Foo17>();
            _container.RegisterType<IFoo18, Foo18>();
            _container.RegisterType<IFoo19, Foo19>();

            _container.RegisterType<IFoo00, Foo00>("1");
            _container.RegisterType<IFoo01, Foo01>("1");
            _container.RegisterType<IFoo02, Foo02>("1");
            _container.RegisterType<IFoo03, Foo03>("1");
            _container.RegisterType<IFoo04, Foo04>("1");
            _container.RegisterType<IFoo05, Foo05>("1");
            _container.RegisterType<IFoo06, Foo06>("1");
            _container.RegisterType<IFoo07, Foo07>("1");
            _container.RegisterType<IFoo08, Foo08>("1");
            _container.RegisterType<IFoo09, Foo09>("1");
            _container.RegisterType<IFoo10, Foo10>("1");
            _container.RegisterType<IFoo11, Foo11>("1");
            _container.RegisterType<IFoo12, Foo12>("1");
            _container.RegisterType<IFoo13, Foo13>("1");
            _container.RegisterType<IFoo14, Foo14>("1");
            _container.RegisterType<IFoo15, Foo15>("1");
            _container.RegisterType<IFoo16, Foo16>("1");
            _container.RegisterType<IFoo17, Foo17>("1");
            _container.RegisterType<IFoo18, Foo18>("1");
            _container.RegisterType<IFoo19, Foo19>("1");

            _container.RegisterFactory<IFoo00>("2", c => new Foo00());
            _container.RegisterFactory<IFoo01>("2", c => new Foo01());
            _container.RegisterFactory<IFoo02>("2", c => new Foo02());
            _container.RegisterFactory<IFoo03>("2", c => new Foo03());
            _container.RegisterFactory<IFoo04>("2", c => new Foo04());
            _container.RegisterFactory<IFoo05>("2", c => new Foo05());
            _container.RegisterFactory<IFoo06>("2", c => new Foo06());
            _container.RegisterFactory<IFoo07>("2", c => new Foo07());
            _container.RegisterFactory<IFoo08>("2", c => new Foo08());
            _container.RegisterFactory<IFoo09>("2", c => new Foo09());
            _container.RegisterFactory<IFoo10>("2", c => new Foo10());
            _container.RegisterFactory<IFoo11>("2", c => new Foo11());
            _container.RegisterFactory<IFoo12>("2", c => new Foo12());
            _container.RegisterFactory<IFoo13>("2", c => new Foo13());
            _container.RegisterFactory<IFoo14>("2", c => new Foo14());
            _container.RegisterFactory<IFoo15>("2", c => new Foo15());
            _container.RegisterFactory<IFoo16>("2", c => new Foo16());
            _container.RegisterFactory<IFoo17>("2", c => new Foo17());
            _container.RegisterFactory<IFoo18>("2", c => new Foo18());
            _container.RegisterFactory<IFoo19>("2", c => new Foo19());

            _container.RegisterInstance(typeof(IFoo<Foo00>), new Foo<Foo00>());
            _container.RegisterInstance(typeof(IFoo<Foo01>), new Foo<Foo01>());
            _container.RegisterInstance(typeof(IFoo<Foo02>), new Foo<Foo02>());
            _container.RegisterInstance(typeof(IFoo<Foo03>), new Foo<Foo03>());
            _container.RegisterInstance(typeof(IFoo<Foo04>), new Foo<Foo04>());
            _container.RegisterInstance(typeof(IFoo<Foo05>), new Foo<Foo05>());
            _container.RegisterInstance(typeof(IFoo<Foo06>), new Foo<Foo06>());
            _container.RegisterInstance(typeof(IFoo<Foo07>), new Foo<Foo07>());
            _container.RegisterInstance(typeof(IFoo<Foo08>), new Foo<Foo08>());
            _container.RegisterInstance(typeof(IFoo<Foo09>), new Foo<Foo09>());
            _container.RegisterInstance(typeof(IFoo<Foo10>), new Foo<Foo10>());
            _container.RegisterInstance(typeof(IFoo<Foo11>), new Foo<Foo11>());
            _container.RegisterInstance(typeof(IFoo<Foo12>), new Foo<Foo12>());
            _container.RegisterInstance(typeof(IFoo<Foo13>), new Foo<Foo13>());
            _container.RegisterInstance(typeof(IFoo<Foo14>), new Foo<Foo14>());
            _container.RegisterInstance(typeof(IFoo<Foo15>), new Foo<Foo15>());
            _container.RegisterInstance(typeof(IFoo<Foo16>), new Foo<Foo16>());
            _container.RegisterInstance(typeof(IFoo<Foo17>), new Foo<Foo17>());
            _container.RegisterInstance(typeof(IFoo<Foo18>), new Foo<Foo18>());
            _container.RegisterInstance(typeof(IFoo<Foo19>), new Foo<Foo19>());
        }


        public virtual object Register()
        {
            _storage[00] = _container.RegisterType(null, typeof(Foo00), null, null);
            _storage[01] = _container.RegisterType(null, typeof(Foo01), null, null);
            _storage[02] = _container.RegisterType(null, typeof(Foo02), null, null);
            _storage[03] = _container.RegisterType(null, typeof(Foo03), null, null);
            _storage[04] = _container.RegisterType(null, typeof(Foo04), null, null);
            _storage[05] = _container.RegisterType(null, typeof(Foo05), null, null);
            _storage[06] = _container.RegisterType(null, typeof(Foo06), null, null);
            _storage[07] = _container.RegisterType(null, typeof(Foo07), null, null);
            _storage[08] = _container.RegisterType(null, typeof(Foo08), null, null);
            _storage[09] = _container.RegisterType(null, typeof(Foo09), null, null);
            _storage[10] = _container.RegisterType(null, typeof(Foo10), null, null);
            _storage[11] = _container.RegisterType(null, typeof(Foo11), null, null);
            _storage[12] = _container.RegisterType(null, typeof(Foo12), null, null);
            _storage[13] = _container.RegisterType(null, typeof(Foo13), null, null);
            _storage[14] = _container.RegisterType(null, typeof(Foo14), null, null);
            _storage[15] = _container.RegisterType(null, typeof(Foo15), null, null);
            _storage[16] = _container.RegisterType(null, typeof(Foo16), null, null);
            _storage[17] = _container.RegisterType(null, typeof(Foo17), null, null);
            _storage[18] = _container.RegisterType(null, typeof(Foo18), null, null);
            _storage[19] = _container.RegisterType(null, typeof(Foo19), null, null);

            return _storage;
        }

        public virtual object RegisterMapping()
        {
            _storage[00] = _container.RegisterType(typeof(IFoo00), typeof(Foo00), null, null);
            _storage[01] = _container.RegisterType(typeof(IFoo01), typeof(Foo01), null, null);
            _storage[02] = _container.RegisterType(typeof(IFoo02), typeof(Foo02), null, null);
            _storage[03] = _container.RegisterType(typeof(IFoo03), typeof(Foo03), null, null);
            _storage[04] = _container.RegisterType(typeof(IFoo04), typeof(Foo04), null, null);
            _storage[05] = _container.RegisterType(typeof(IFoo05), typeof(Foo05), null, null);
            _storage[06] = _container.RegisterType(typeof(IFoo06), typeof(Foo06), null, null);
            _storage[07] = _container.RegisterType(typeof(IFoo07), typeof(Foo07), null, null);
            _storage[08] = _container.RegisterType(typeof(IFoo08), typeof(Foo08), null, null);
            _storage[09] = _container.RegisterType(typeof(IFoo09), typeof(Foo09), null, null);
            _storage[10] = _container.RegisterType(typeof(IFoo10), typeof(Foo10), null, null);
            _storage[11] = _container.RegisterType(typeof(IFoo11), typeof(Foo11), null, null);
            _storage[12] = _container.RegisterType(typeof(IFoo12), typeof(Foo12), null, null);
            _storage[13] = _container.RegisterType(typeof(IFoo13), typeof(Foo13), null, null);
            _storage[14] = _container.RegisterType(typeof(IFoo14), typeof(Foo14), null, null);
            _storage[15] = _container.RegisterType(typeof(IFoo15), typeof(Foo15), null, null);
            _storage[16] = _container.RegisterType(typeof(IFoo16), typeof(Foo16), null, null);
            _storage[17] = _container.RegisterType(typeof(IFoo17), typeof(Foo17), null, null);
            _storage[18] = _container.RegisterType(typeof(IFoo18), typeof(Foo18), null, null);
            _storage[19] = _container.RegisterType(typeof(IFoo19), typeof(Foo19), null, null);

            return _storage;
        }

        public virtual object RegisterInstance()
        {
            _storage[00] = _container.RegisterInstance(null, null, instance00, null);
            _storage[01] = _container.RegisterInstance(null, null, instance01, null);
            _storage[02] = _container.RegisterInstance(null, null, instance02, null);
            _storage[03] = _container.RegisterInstance(null, null, instance03, null);
            _storage[04] = _container.RegisterInstance(null, null, instance04, null);
            _storage[05] = _container.RegisterInstance(null, null, instance05, null);
            _storage[06] = _container.RegisterInstance(null, null, instance06, null);
            _storage[07] = _container.RegisterInstance(null, null, instance07, null);
            _storage[08] = _container.RegisterInstance(null, null, instance08, null);
            _storage[09] = _container.RegisterInstance(null, null, instance09, null);
            _storage[10] = _container.RegisterInstance(null, null, instance10, null);
            _storage[11] = _container.RegisterInstance(null, null, instance11, null);
            _storage[12] = _container.RegisterInstance(null, null, instance12, null);
            _storage[13] = _container.RegisterInstance(null, null, instance13, null);
            _storage[14] = _container.RegisterInstance(null, null, instance14, null);
            _storage[15] = _container.RegisterInstance(null, null, instance15, null);
            _storage[16] = _container.RegisterInstance(null, null, instance16, null);
            _storage[17] = _container.RegisterInstance(null, null, instance17, null);
            _storage[18] = _container.RegisterInstance(null, null, instance18, null);
            _storage[19] = _container.RegisterInstance(null, null, instance19, null);

            return _storage;
        }

        public virtual object Registrations()
        {
            _storage[00] = _container.Registrations.ToArray();
            _storage[01] = _container.Registrations.ToArray();
            _storage[02] = _container.Registrations.ToArray();
            _storage[03] = _container.Registrations.ToArray();
            _storage[04] = _container.Registrations.ToArray();
            _storage[05] = _container.Registrations.ToArray();
            _storage[06] = _container.Registrations.ToArray();
            _storage[07] = _container.Registrations.ToArray();
            _storage[08] = _container.Registrations.ToArray();
            _storage[09] = _container.Registrations.ToArray();
            _storage[10] = _container.Registrations.ToArray();
            _storage[11] = _container.Registrations.ToArray();
            _storage[12] = _container.Registrations.ToArray();
            _storage[13] = _container.Registrations.ToArray();
            _storage[14] = _container.Registrations.ToArray();
            _storage[15] = _container.Registrations.ToArray();
            _storage[16] = _container.Registrations.ToArray();
            _storage[17] = _container.Registrations.ToArray();
            _storage[18] = _container.Registrations.ToArray();
            _storage[19] = _container.Registrations.ToArray();

            return _storage;
        }

        public virtual object IsRegistered()
        {
            _storage[00] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[01] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[02] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[03] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[04] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[05] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[06] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[07] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[08] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[09] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[10] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[11] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[12] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[13] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[14] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[15] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[16] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[17] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[18] = _container.IsRegistered(typeof(IUnityContainer));
            _storage[19] = _container.IsRegistered(typeof(IUnityContainer));

            return _storage;
        }

        public virtual object IsRegisteredFalse()
        {
            _storage[00] = _container.IsRegistered(typeof(IFoo));
            _storage[01] = _container.IsRegistered(typeof(IFoo));
            _storage[02] = _container.IsRegistered(typeof(IFoo));
            _storage[03] = _container.IsRegistered(typeof(IFoo));
            _storage[04] = _container.IsRegistered(typeof(IFoo));
            _storage[05] = _container.IsRegistered(typeof(IFoo));
            _storage[06] = _container.IsRegistered(typeof(IFoo));
            _storage[07] = _container.IsRegistered(typeof(IFoo));
            _storage[08] = _container.IsRegistered(typeof(IFoo));
            _storage[09] = _container.IsRegistered(typeof(IFoo));
            _storage[10] = _container.IsRegistered(typeof(IFoo));
            _storage[11] = _container.IsRegistered(typeof(IFoo));
            _storage[12] = _container.IsRegistered(typeof(IFoo));
            _storage[13] = _container.IsRegistered(typeof(IFoo));
            _storage[14] = _container.IsRegistered(typeof(IFoo));
            _storage[15] = _container.IsRegistered(typeof(IFoo));
            _storage[16] = _container.IsRegistered(typeof(IFoo));
            _storage[17] = _container.IsRegistered(typeof(IFoo));
            _storage[18] = _container.IsRegistered(typeof(IFoo));
            _storage[19] = _container.IsRegistered(typeof(IFoo));

            return _storage;
        }
    }
}
