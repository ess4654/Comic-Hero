using System.Linq;
using UnityEngine;
using static ComicHero.Views.ComicPanel;

namespace ComicHero.Views
{
    /// <summary>
    ///     Randomizes the graphics for each comic panel.
    /// </summary>
    [RequireComponent (typeof(ComicPanel))]
    public class RandomizedComicPanel : MonoBehaviour
    {
        #region VARIABLE DECLARATIONS

        [SerializeField] private FillType[] randomFills = FillTypeOptions.Where(x => x != FillType.Texture).ToArray();
        [SerializeField] private GradientColors[] gradientPresets = new[]
        {
            //Yellow
            new GradientColors
            (
                new Color (0.70588f, 0.69411f, 0.11372f),
                new Color (0.67058f, 0.58431f, 0.16078f)
            ),

            //Red
            new GradientColors
            (
                new Color (0.74509f, 0.14901f, 0.07450f),
                new Color (1.00000f, 0.23270f, 0.13679f)
            ),

            //Blue
            new GradientColors
            (
                new Color (0.16470f, 0.56078f, 0.67450f),
                new Color (0.11764f, 0.50196f, 0.71372f)
            ),

            //Purple
            new GradientColors
            (
                new Color (0.52549f, 0.11764f, 0.70980f),
                new Color (0.44705f, 0.16470f, 0.67450f)
            ),

            //Orange
            new GradientColors
            (
                new Color (0.70588f, 0.43529f, 0.11372f),
                new Color (0.67450f, 0.36862f, 0.16470f)
            ),

            //Green
            new GradientColors
            (
                new Color (0.11764f, 0.71764f, 0.58823f),
                new Color (0.16078f, 0.62352f, 0.46666f)
            ),

            //Greyscale 1
            new GradientColors
            (
                new Color (0.36078f, 0.36078f, 0.36078f),
                new Color (0.32941f, 0.32941f, 0.32941f)
            ),

            //Greyscale 2
            new GradientColors
            (
                new Color (0.81568f, 0.81568f, 0.81568f),
                new Color (0.77647f, 0.77647f, 0.77647f)
            )
        };
        [SerializeField] private float gradientFlipProbability = 0.5f;

        private GradientColors RandomGradientColor =>
            gradientPresets.Length == 0 ?
            new GradientColors(Color.white, Color.white) : gradientPresets.SelectRandom();

        private Color RadomSolidColor =>
            Random.value < 0.5 ?
            RandomGradientColor.color1 :
            RandomGradientColor.color2;

        private ComicPanel comic;

        #endregion

        private void Start()
        {
            comic = GetComponent<ComicPanel>();
            var randomFill = randomFills.Where(x => x != FillType.Texture).SelectRandom();
            comic.Fill = randomFill;

            if (randomFill != FillType.Solid)
                SetRandomGradient();

            if (randomFill == FillType.Solid)
            {
                comic.SolidColor = RadomSolidColor;
            }
            else if(randomFill == FillType.Gradient)
            {
            }
            else if(randomFill == FillType.Stripes)
            {
            }
            else if (randomFill == FillType.Checkerboard)
            {
            }
            else if (randomFill == FillType.Radial)
            {
            }
            else if (randomFill == FillType.Stars)
            {
            }
            else if (randomFill == FillType.PokaDots)
            {
            }
        }

        private void SetRandomGradient()
        {
            var rand = Random.value;
            var randomGradient = RandomGradientColor;
            if(rand < Mathf.Clamp01(gradientFlipProbability))
                comic.GradientColor = new GradientColors(randomGradient.color2, randomGradient.color1);
            else
                comic.GradientColor= randomGradient;
        }
    }
}