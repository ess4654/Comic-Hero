using UnityEngine;
using static ComicHero.Views.Comic.ComicPanel;

namespace ComicHero.Views.Comic
{
    /// <summary>
    ///     Randomizes the graphics for each comic panel.
    /// </summary>
    public class RandomizedComicPanel : ComicComponent
    {
        #region VARIABLE DECLARATIONS

        [SerializeField] private FillType[] randomFills = FillTypeOptions;

        [Header("Gradient")]
        [SerializeField] private GradientColors[] gradientPresets = new[]
        {
            //Yellow 1
            new GradientColors
            (
                new Color (0.70588f, 0.69411f, 0.11372f),
                new Color (0.67058f, 0.58431f, 0.16078f)
            ),

            //Yellow 2
            new GradientColors
            (
                new Color (0.93333f, 0.81568f, 0.00000f),
                new Color (0.86666f, 0.65882f, 0.00000f)
            ),

            //Red
            new GradientColors
            (
                new Color (0.74509f, 0.14901f, 0.07450f),
                new Color (1.00000f, 0.23270f, 0.13679f)
            ),

            //Blue 1
            new GradientColors
            (
                new Color (0.16470f, 0.56078f, 0.67450f),
                new Color (0.11764f, 0.50196f, 0.71372f)
            ),

            //Blue 2
            new GradientColors
            (
                new Color (0.10588f, 0.18823f, 0.67450f),
                new Color (0.15294f, 0.28235f, 0.64313f)
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
            ),

            //Greyscale 3
            new GradientColors
            (
                new Color (0.18823f, 0.18823f, 0.18823f),
                new Color (0.15686f, 0.15686f, 0.15686f)
            )
        };
        [SerializeField] private float gradientFlipProbability = 0.5f;

        [Header("Checkerboard")]
        [SerializeField] private float randomMaxRotation = 0.45f;
        [SerializeField] private Vector2 randomCheckerboardScale = new Vector2(0.25f, 3f);
        
        [Header("Radial")]
        [SerializeField] private Vector2 randomLineWidth = new Vector2(0.5f, 1.5f);
        [SerializeField] private Vector2 randomLineSpacing = new Vector2(0.35f, 1.0f);

        [Header("Radial")]
        [SerializeField] private float radialMoveProbability = 0.5f;
        [SerializeField] private Vector2 randomRadialOffsetX = new Vector2(-1.0f, 1.0f);
        [SerializeField] private Vector2 randomRadialOffsetY = new Vector2(-1.0f, 1.0f);
        
        [Header("Texture")]
        [SerializeField] private Texture[] randomFillTextures;

        private GradientColors RandomGradientColor =>
            gradientPresets.Length == 0 ?
            new GradientColors(Color.white, Color.white) : gradientPresets.SelectRandom();

        private Color RadomSolidColor =>
            Random.value < 0.5 ?
            RandomGradientColor.color1 :
            RandomGradientColor.color2;

        private float RandomCheckerboardSize =>
            Random.Range(randomCheckerboardScale.x, randomCheckerboardScale.y);
        
        private float RandomLineWidth =>
            Random.Range(randomLineWidth.x, randomLineWidth.y);

        private float RandomLineSpacing =>
            Random.Range(randomLineSpacing.x, randomLineSpacing.y);

        private float RandomRotation
        {
            get
            {
                var rand = Random.value;
                var rValue = randomMaxRotation * (rand < 0.5f ? 1 : -1);

                return Random.Range(rValue > 0f ? 0f : rValue, rValue > 0f ? rValue : 0f);
            }
        }

        private Vector2 RandomRadialOffset => new Vector2
        (
            Random.Range(randomRadialOffsetX.x, randomRadialOffsetX.y),
            Random.Range(randomRadialOffsetY.x, randomRadialOffsetY.y)
        );

        #endregion

        #region ENGINE

        protected override float TweenTime => 1;

        protected override void Tween(float lerp) { }

        private void OnEnable()
        {
            //get a random fill type for the graphic background
            var randomFill = randomFills.SelectRandom();
            while(randomFill == FillType.Texture && randomFillTextures.Length == 0)
                randomFill = randomFills.SelectRandom();

            comic.Fill = randomFill;

            if (randomFill != FillType.Solid)
                SetRandomGradient();

            if (randomFill == FillType.Solid)
            {
                comic.SolidColor = RadomSolidColor;
            }
            else if(randomFill == FillType.Gradient)
            {
                comic.Rotation = randomMaxRotation;
            }
            else if(randomFill == FillType.Stripes)
            {
                comic.Rotation = RandomRotation;
                comic.LineThickness = RandomLineWidth;
                comic.LineSpacing = RandomLineSpacing;
            }
            else if (randomFill == FillType.Checkerboard)
            {
                comic.Rotation = RandomRotation;
                comic.Size = RandomCheckerboardSize;
            }
            else if (randomFill == FillType.Radial)
            {
                comic.Rotation = Random.Range(0f, 360f);
                comic.LineThickness = RandomLineWidth;
                
                if(Random.value < Mathf.Clamp01(gradientFlipProbability))
                    comic.Offset = RandomRadialOffset;
            }
            else if (randomFill == FillType.Stars)
            {
                comic.Rotation = RandomRotation;
                comic.Size = RandomCheckerboardSize;
            }
            else if (randomFill == FillType.PokaDots)
            {
                comic.Rotation = RandomRotation;
                comic.Size = RandomCheckerboardSize;
            }
            else if(randomFill == FillType.Texture)
            {
                if(randomFillTextures.Length > 0)
                    comic.FillTexture = randomFillTextures.SelectRandom();
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

        #endregion
    }
}