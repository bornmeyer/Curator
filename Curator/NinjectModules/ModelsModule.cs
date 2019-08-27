using Curator.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.NinjectModules
{
    public class ModelsModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IConfigFilePathLocator>().To<ConfigFilePathLocator>();
            Kernel.Bind<IFileConfigurationReader>().To<FileConfigurationReader>();
            Kernel.Bind<IFileConfigurationWriter>().To<FileConfigurationWriter>();
            Kernel.Bind<IFileManager>().To<FileManager>();
            Kernel.Bind<IFileWatcherFactory>().To<FileWatcherFactory>();
            Kernel.Bind<IFileWatcher>().To<FileWatcher>();
            Kernel.Bind<IFileHandlingStrategySelector>().To<FileHandlingStrategySelector>();
            Kernel.Bind<IFileHandlingStrategy>().To<EmptyLogFileHandlingStrategy>();
            Kernel.Bind<IFileHandlingStrategy>().To<AppendLogFileHandlingStrategy>();
            Kernel.Bind<IArchiveManager>().To<ArchiveManager>();
            Kernel.Bind<ISignatureCreator>().To<SignatureCreator>();
            Kernel.Bind<IDeltaCreator>().To<DeltaCreator>();
            Kernel.Bind<IDeltaPatcher>().To<DeltaPatcher>();
            Kernel.Bind<IDeltaCreationStrategy>().To<FirstDeltaCreationStrategy>();
            Kernel.Bind<IDeltaCreationStrategy>().To<AggregatedDeltaCreationStrategy>();
            Kernel.Bind<IFileRestore>().To<FileRestore>();
            Kernel.Bind<ITransactionWriter>().To<TransactionWriter>();
        }
    }
}
