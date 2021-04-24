using DevExpress.Mvvm;
using mnd.Logic.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.MyControls
{
    public class Step : MyBindableBase
    {
        private string ısColored;
        private bool yetkiliMi;

        public bool IsActive { get; set; }
        public string Text { get; set; }
        public int Index { get; set; }
        public string OnayDurum { get => ısColored; set => SetProperty(ref ısColored, value); }
        public int LineHeight { get; internal set; }
        public string LeftData { get; internal set; }
        public string RightData { get; internal set; }
        public string SurecAdim { get; internal set; }

        public bool YetkiliMi { get => yetkiliMi; set => SetProperty(ref yetkiliMi, value); }
        public string YetkiliKullanici { get; internal set; }
    }
    public class StepControlModel : MyBindableBase
    {
        private ObservableCollection<Step> steps;
        private Step selectedStep;

        public ObservableCollection<Step> Steps { get => steps; set => SetProperty(ref steps, value); }

        public DelegateCommand OnaylaCommand => new DelegateCommand(OnOnayla, true);

        public DelegateCommand ReddetCommand => new DelegateCommand(OnReddet, true);

        private void OnOnayla()
        {
            Next();
        }
        private void OnReddet()
        {
            Previous();
        }



        public Step SelectedStep
        {
            get => selectedStep;
            set
            {
                SetProperty(ref selectedStep, value);

                Steps.Where(c => c.Index <= value.Index).ToList().ForEach(c => { c.OnayDurum = "Onaylandı"; });

                Steps.Where(c => c.Index > value.Index).ToList().ForEach(c => { c.OnayDurum = "Onaylanacak"; });

                value.OnayDurum = "Onayda";

            }
        }

        public void Next()
        {
            var index = SelectedStep.Index + 1;
            if (index + 1 > Steps.Count) index = Steps.Count - 1;

            SelectedStep = Steps[index - 1];
        }

        public void Previous()
        {
            var index = SelectedStep.Index - 1;
            if (index < 0) index = 0;

            SelectedStep = Steps[index - 1];
        }
        public StepControlModel()
        {
            Steps = new ObservableCollection<Step>();

        }
    }
}
