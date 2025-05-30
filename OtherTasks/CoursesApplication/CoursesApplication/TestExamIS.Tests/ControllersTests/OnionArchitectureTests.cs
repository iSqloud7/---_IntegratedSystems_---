using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;
using TestExamIS.Tests.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TestExamIS.Tests.ArchitectureTests
{
    [Collection("Test Suite")]
    public class OnionArchitectureTests : LoggedTestBase
    {
        private readonly Assembly[] _allAssemblies;
        private readonly Type _dbContextBaseType = typeof(IdentityDbContext);

        public OnionArchitectureTests(GlobalTestFixture fixture) : base(fixture)
        {
            _allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        private void LogDiagnosticInfo()
        {
            Console.WriteLine("Loaded assemblies:");
            foreach (var assembly in _allAssemblies)
            {
                Console.WriteLine($" - {assembly.FullName}");
            }
        }

        private IEnumerable<Type> GetControllers()
        {
            return _allAssemblies
                .SelectMany(a => 
                {
                    try { return a.GetTypes(); } 
                    catch { return Type.EmptyTypes; }
                })
                .Where(t => t.Name.EndsWith("Controller"));
        }

        private IEnumerable<Type> GetServices()
        {
            return _allAssemblies
                .SelectMany(a => 
                {
                    try { return a.GetTypes(); } 
                    catch { return Type.EmptyTypes; }
                })
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));
        }

        private IEnumerable<Type> GetServiceInterfaces()
        {
            return _allAssemblies
                .SelectMany(a => 
                {
                    try { return a.GetTypes(); } 
                    catch { return Type.EmptyTypes; }
                })
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Service"));
        }

        private IEnumerable<Type> GetRepositoryInterfaces()
        {
            return _allAssemblies
                .SelectMany(a => 
                {
                    try { return a.GetTypes(); } 
                    catch { return Type.EmptyTypes; }
                })
                .Where(t => t.IsInterface && t.Name.StartsWith("I") && t.Name.EndsWith("Repository"));
        }

        [LoggedFact(Category = "OnionArchitecture", Points = 10)]
        public void Controllers_Should_Not_Depend_On_ApplicationDbContext()
        {
            RunTest(() =>
            {
                var controllerTypes = GetControllers().ToList();
                Console.WriteLine($"Found {controllerTypes.Count} controller types");

                var test = _allAssemblies
                    .SelectMany(a =>
                    {
                        try { return a.GetTypes(); }
                        catch { return Type.EmptyTypes; }
                    }).Where(x => x.Name.StartsWith("A")).OrderBy(x => x.Name);
                var dbContextType = _allAssemblies
                    .SelectMany(a => 
                    {
                        try { return a.GetTypes(); } 
                        catch { return Type.EmptyTypes; }
                    })
                    .FirstOrDefault(t => t.Name.Contains("ApplicationDbContext"));

                if (dbContextType == null)
                {
                    dbContextType = _allAssemblies
                        .SelectMany(a => 
                        {
                            try { return a.GetTypes(); } 
                            catch { return Type.EmptyTypes; }
                        })
                        .FirstOrDefault(t => _dbContextBaseType.IsAssignableFrom(t) && t != _dbContextBaseType);
                }

                if (dbContextType == null)
                {
                    LogDiagnosticInfo();
                    Assert.True(false, "Could not find ApplicationDbContext or any DbContext subclass. Check if Entity Framework Core is properly referenced.");
                }

                Console.WriteLine($"Using DbContext type: {dbContextType.FullName}");

                var violations = new List<string>();

                foreach (var controllerType in controllerTypes)
                {
                    var constructors = controllerType.GetConstructors();
                    
                    foreach (var constructor in constructors)
                    {
                        var parameters = constructor.GetParameters();
                        
                        foreach (var parameter in parameters)
                        {
                            if (parameter.ParameterType == dbContextType || 
                                _dbContextBaseType.IsAssignableFrom(parameter.ParameterType))
                            {
                                violations.Add($"Controller '{controllerType.Name}' has a direct dependency on '{parameter.ParameterType.Name}'");
                            }
                        }
                    }
                }

                if (violations.Any())
                {
                    // True -> False
                    Assert.False(false, $"Found {violations.Count} architectural violations:\n" + string.Join("\n", violations));
                }
            });
        }

        [LoggedFact(Category = "OnionArchitecture", Points = 10)]
        public void Services_Should_Not_Depend_On_ApplicationDbContext()
        {
            RunTest(() =>
            {
                var serviceTypes = GetServices().ToList();
                Console.WriteLine($"Found {serviceTypes.Count} service types");

                var dbContextType = _allAssemblies
                    .SelectMany(a => 
                    {
                        try { return a.GetTypes(); } 
                        catch { return Type.EmptyTypes; }
                    })
                    .FirstOrDefault(t => t.Name == "ApplicationDbContext");

                if (dbContextType == null)
                {
                    dbContextType = _allAssemblies
                        .SelectMany(a => 
                        {
                            try { return a.GetTypes(); } 
                            catch { return Type.EmptyTypes; }
                        })
                        .FirstOrDefault(t => _dbContextBaseType.IsAssignableFrom(t) && t != _dbContextBaseType);
                }

                if (dbContextType == null)
                {
                    LogDiagnosticInfo();
                    Assert.True(false, "Could not find ApplicationDbContext or any DbContext subclass. Check if Entity Framework Core is properly referenced.");
                }

                Console.WriteLine($"Using DbContext type: {dbContextType.FullName}");

                var violations = new List<string>();

                foreach (var serviceType in serviceTypes)
                {
                    var constructors = serviceType.GetConstructors();
                    
                    foreach (var constructor in constructors)
                    {
                        var parameters = constructor.GetParameters();
                        
                        foreach (var parameter in parameters)
                        {
                            if (parameter.ParameterType == dbContextType || 
                                _dbContextBaseType.IsAssignableFrom(parameter.ParameterType))
                            {
                                violations.Add($"Service '{serviceType.Name}' has a direct dependency on '{parameter.ParameterType.Name}'");
                            }
                        }
                    }
                }

                if (violations.Any())
                {
                    // True -> False
                    Assert.False(false, $"Found {violations.Count} architectural violations:\n" + string.Join("\n", violations));
                }
            });
        }

        [LoggedFact(Category = "OnionArchitecture", Points = 10)]
        public void Controllers_Should_Not_Depend_On_IRepository()
        {
            RunTest(() =>
            {
                var controllerTypes = GetControllers().ToList();
                Console.WriteLine($"Found {controllerTypes.Count} controller types");

                var repositoryInterfaceTypes = GetRepositoryInterfaces().ToList();
                Console.WriteLine($"Found {repositoryInterfaceTypes.Count} repository interface types");

                if (!repositoryInterfaceTypes.Any())
                {
                    var possibleRepositoryTypes = _allAssemblies
                        .SelectMany(a => 
                        {
                            try { return a.GetTypes(); } 
                            catch { return Type.EmptyTypes; }
                        })
                        .Where(t => t.IsInterface && t.Name.Contains("Repository"))
                        .ToList();
                        
                    Console.WriteLine($"Found {possibleRepositoryTypes.Count} types containing 'Repository' in their name:");
                    foreach (var type in possibleRepositoryTypes)
                    {
                        Console.WriteLine($" - {type.FullName}");
                    }
                }

                var violations = new List<string>();

                foreach (var controllerType in controllerTypes)
                {
                    var constructors = controllerType.GetConstructors();
                    
                    foreach (var constructor in constructors)
                    {
                        var parameters = constructor.GetParameters();
                        
                        foreach (var parameter in parameters)
                        {
                            if (repositoryInterfaceTypes.Contains(parameter.ParameterType) ||
                                repositoryInterfaceTypes.Any(ri => ri.IsAssignableFrom(parameter.ParameterType)))
                            {
                                violations.Add($"Controller '{controllerType.Name}' has a direct dependency on repository interface '{parameter.ParameterType.Name}'");
                            }
                        }
                    }
                }

                if (violations.Any())
                {
                    Assert.Fail($"Found {violations.Count} architectural violations:\n" + string.Join("\n", violations));
                }
            });
        }
    }
}