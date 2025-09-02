using BrownianMotion.ViewModels;

namespace BrownianMotion.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void Constructor_ShouldGenerateSimulations()
        {
            var vm = new MainViewModel();

            Assert.Equal(vm.NumSimulations, vm.Simulations.Count);
        }

        [Fact]
        public void Properties_ShouldHaveExpectedDefaults()
        {
            var vm = new MainViewModel();

            Assert.Equal(20, vm.Sigma);
            Assert.Equal(2, vm.Mean);
            Assert.Equal(100.0, vm.InitialPrice);
            Assert.Equal(252, vm.NumDays);
            Assert.Equal(5, vm.NumSimulations);
        }

        [Fact]
        public void GenerateSimulationCommand_ShouldRegenerateSimulations()
        {
            var vm = new MainViewModel();

            var firstRun = vm.Simulations.Select(s => s.ToArray()).ToList();
            vm.GenerateSimulationCommand.Execute(null);
            var secondRun = vm.Simulations.Select(s => s.ToArray()).ToList();

            Assert.Equal(vm.NumSimulations, secondRun.Count);
            Assert.NotEqual(firstRun[0][1], secondRun[0][1]);
        }

        [Fact]
        public void Simulations_ShouldContainArraysOfCorrectLength()
        {
            var vm = new MainViewModel();

            Assert.All(vm.Simulations, sim => Assert.Equal(vm.NumDays, sim.Length));
        }
    }
}
