using System.Web.Mvc;
using Tomelt.DisplayManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Localization.Services;
using Tomelt.Localization.ViewModels;
using Tomelt.UI.Admin;

namespace Tomelt.Localization.Controllers {
    [TomeltFeature("Tomelt.Localization.Transliteration")]
    [Admin, ValidateInput(false)]
    public class TransliterationAdminController : Controller {
        private readonly ITransliterationService _transliterationService;

        public TransliterationAdminController(ITransliterationService transliterationService,
            IShapeFactory shapeFactory) {
            _transliterationService = transliterationService;
            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public ActionResult Index() {

            var specifications = _transliterationService.GetSpecifications();

            var viewModel = Shape.ViewModel()
                .Specifications(specifications);

            return View(viewModel);
        }

        public ActionResult Create() {
            var viewModel = new CreateTransliterationViewModel();

            return View(viewModel);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(CreateTransliterationViewModel viewModel) {

            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            _transliterationService.Create(viewModel.CultureFrom, viewModel.CultureTo, viewModel.Rules);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var record = _transliterationService.Get(id);

            var viewModel = new EditTransliterationViewModel {
                Id = record.Id,
                CultureFrom = record.CultureFrom,
                CultureTo = record.CultureTo,
                Rules = record.Rules
            };

            return View(viewModel);            
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(EditTransliterationViewModel viewModel) {

            if (!ModelState.IsValid) {
                return View(viewModel);
            }

            _transliterationService.Update(viewModel.Id, viewModel.CultureFrom, viewModel.CultureTo, viewModel.Rules);

            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id) {
            _transliterationService.Remove(id);

            return RedirectToAction("Index");
        }
    }
}