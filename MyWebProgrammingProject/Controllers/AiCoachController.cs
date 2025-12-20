using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebProgrammingProject.Models;
using MyWebProgrammingProject.Services;

namespace MyWebProgrammingProject.Controllers
{
    [Authorize(Roles = "Member")]
    public class AiCoachController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAiRecommendationService _ai;

        public AiCoachController(UserManager<ApplicationUser> userManager, IAiRecommendationService ai)
        {
            _userManager = userManager;
            _ai = ai;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var vm = new AiCoachViewModel
            {
                HeightCm = user.Height is null ? 170 : (int)Math.Round(user.Height.Value),
                WeightKg = user.Weight is null ? 70 : (int)Math.Round(user.Weight.Value),
                BodyType = user.BodyType ?? "",
                Goal = user.Goal ?? ""
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AiCoachViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Kullanıcı profil alanlarına kaydet
            user.Height = vm.HeightCm;
            user.Weight = vm.WeightKg;
            user.BodyType = vm.BodyType;
            user.Goal = vm.Goal;

            var update = await _userManager.UpdateAsync(user);
            if (!update.Succeeded)
            {
                foreach (var err in update.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);

                return View(vm);
            }

            vm.AiResult = await _ai.GetWorkoutAndDietAsync(
                heightCm: vm.HeightCm,
                weightKg: vm.WeightKg,
                bodyType: vm.BodyType,
                goal: vm.Goal,
                cancellationToken: cancellationToken);

            return View(vm);
        }
    }
}
