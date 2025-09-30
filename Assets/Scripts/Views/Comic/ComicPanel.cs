using System;
using UnityEngine;

namespace ComicHero.Views.Comic
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ComicPanel : MonoBehaviour
    {
        #region VARIABLE DECLARATIONS

        #region DEFINITIONS

        /// <summary>
        ///     Defines the types of fill that can be used for the comic panel.
        /// </summary>
        public enum FillType
        {
            Solid,
            Gradient,
            Texture,
            Stripes,
            Checkerboard,
            Radial,
            Stars,
            PokaDots
        }

        /// <summary>
        ///     List of all fill type options.
        /// </summary>
        public static readonly FillType[] FillTypeOptions = new FillType[]
        {
            FillType.Solid,
            FillType.Gradient,
            FillType.Texture,
            FillType.Stripes,
            FillType.Checkerboard,
            FillType.Radial,
            FillType.Stars,
            FillType.PokaDots
        };

        [Serializable]
        public class GradientColors
        {
            public Color color1 = Color.white;
            public Color color2 = Color.black;

            public GradientColors() { }

            public GradientColors(Color color1, Color color2)
            {
                this.color1 = color1;
                this.color2 = color2;
            }
        }

        [Serializable]
        public class GradientSetting : GradientColors
        {
            public float offset;
            public float rotation;
        }

        [Serializable]
        public class TilingGradient : GradientColors
        {
            public Vector2 offset;
        }

        [Serializable]
        public class StripeSetting : GradientColors
        {
            public float lineWidth = 1;
            public float spacing = 1;
            public float offset;
            public float rotation;
        }

        [Serializable]
        public class CheckerboardSetting : TilingGradient
        {
            public float scale = 1;
            public float rotation;
        }

        [Serializable]
        public class RadialSetting : TilingGradient
        {
            public float lineThickness = 1;
            public float rotation;
            public Vector2 size = Vector2.one;
        }

        #endregion

        /// <summary>
        ///     Called when the comic strip has been rendered.
        /// </summary>
        public event Action<FillType> OnComicRendered;

        [SerializeField] private FillType fillType;

        [Header("Color")]
        [SerializeField] private Color solidColor = Color.white;
        [SerializeField] private GradientSetting gradientColor;
        [SerializeField] private float hueShift;
        [SerializeField] private float saturation = 1;
        [SerializeField] private float contrast = 1;
        [SerializeField] private float brightness = 1;
        [SerializeField] private bool invert;

        [Header("Texture")]
        [SerializeField] private Texture texture;
        [SerializeField] private float textureRotation;
        [SerializeField] private Vector2 textureScale = Vector2.one;
        [SerializeField] private Vector2 textureOffset;

        [Header("Stripes")]
        [SerializeField] private StripeSetting stripe;

        [Header("Checkerboard")]
        [SerializeField] private CheckerboardSetting checkerboard;
        
        [Header("Radial")]
        [SerializeField] private RadialSetting radial;

        [Header("Stars")]
        [SerializeField] private CheckerboardSetting stars;

        [Header("Poka Dots")]
        [SerializeField] private CheckerboardSetting pokaDots;

        #region GETTERS & SETTERS

        /// <summary>
        ///     The fill type used by the comic panel.
        /// </summary>
        public FillType Fill
        {
            get => fillType;
            set
            {
                fillType = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Solid fill color.
        /// </summary>
        public Color SolidColor
        {
            get => solidColor;
            set
            {
                solidColor = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Gradient colors used by panel graphics.
        /// </summary>
        public GradientColors GradientColor
        {
            get
            {
                if (fillType == FillType.Gradient)
                    return gradientColor;
                else if (fillType == FillType.Stripes)
                    return stripe;
                else if (fillType == FillType.Checkerboard)
                    return checkerboard;
                else if (fillType == FillType.Radial)
                    return radial;
                else if (fillType == FillType.Stars)
                    return stars;
                else if (fillType == FillType.PokaDots)
                    return pokaDots;

                return null;
            }
            set
            {
                if (fillType == FillType.Gradient)
                {
                    gradientColor.color1 = value.color1;
                    gradientColor.color2 = value.color2;
                }
                else if (fillType == FillType.Stripes)
                {
                    stripe.color1 = value.color1;
                    stripe.color2 = value.color2;
                }
                else if (fillType == FillType.Checkerboard)
                {
                    checkerboard.color1 = value.color1;
                    checkerboard.color2 = value.color2;
                }
                else if (fillType == FillType.Radial)
                {
                    radial.color1 = value.color1;
                    radial.color2 = value.color2;
                }
                else if (fillType == FillType.Stars)
                {
                    stars.color1 = value.color1;
                    stars.color2 = value.color2;
                }
                else if (fillType == FillType.PokaDots)
                {
                    pokaDots.color1 = value.color1;
                    pokaDots.color2 = value.color2;
                }
             
                isDirty = true;
            }
        }

        /// <summary>
        ///     The offset value of the gradient color.
        /// </summary>
        public float GradientOffset
        {
            get
            {
                if (fillType == FillType.Gradient)
                    return gradientColor.offset;

                return 0;
            }
            set
            {
                if (fillType == FillType.Gradient)
                {
                    gradientColor.offset = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     Gradient fill color.
        /// </summary>
        public GradientSetting Gradient
        {
            get => gradientColor;
            set
            {
                gradientColor = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Shifts the hue of the comic panel.
        /// </summary>
        public float Hue
        {
            get => hueShift;
            set
            {
                hueShift = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Shifts the saturation of the comic panel.
        /// </summary>
        public float Saturation
        {
            get => hueShift;
            set
            {
                saturation = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Shifts the contrast of the comic panel.
        /// </summary>
        public float Contrast
        {
            get => contrast;
            set
            {
                contrast = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Inverts the color of the comic panel.
        /// </summary>
        public bool Invert
        {
            get => invert;
            set
            {
                invert = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Shifts the brightness of the comic panel.
        /// </summary>
        public float Brightness
        {
            get => brightness;
            set
            {
                brightness = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Texture used as a fill for the comic.
        /// </summary>
        public Texture FillTexture
        {
            get => texture;
            set
            {
                texture = value;
                isDirty = true;
            }
        }

        /// <summary>
        ///     Rotation of the comic fill.
        /// </summary>
        public float Rotation
        {
            get
            {
                if (fillType == FillType.Gradient)
                    return gradientColor.rotation;
                else if (fillType == FillType.Texture)
                    return textureRotation;
                else if (fillType == FillType.Stripes)
                    return stripe.rotation;
                else if (fillType == FillType.Checkerboard)
                    return checkerboard.rotation;
                else if (fillType == FillType.Radial)
                    return radial.rotation;
                else if (fillType == FillType.Stars)
                    return stars.rotation;
                else if (fillType == FillType.PokaDots)
                    return pokaDots.rotation;
                
                return 0;
            }
            set
            {
                if (fillType == FillType.Gradient)
                    gradientColor.rotation = value;
                else if (fillType == FillType.Texture)
                    textureRotation = value;
                else if (fillType == FillType.Stripes)
                    stripe.rotation = value;
                else if (fillType == FillType.Checkerboard)
                    checkerboard.rotation = value;
                else if (fillType == FillType.Radial)
                    radial.rotation = value;
                else if (fillType == FillType.Stars)
                    stars.rotation = value;
                else if (fillType == FillType.PokaDots)
                    pokaDots.rotation = value;

                isDirty = true;
            }
        }

        /// <summary>
        ///     Scale of checkerboard patterns.
        /// </summary>
        public Vector2 Scale
        {
            get
            {
                if(fillType == FillType.Texture)
                    return textureScale;
                else if(fillType == FillType.Radial)
                    return radial.size;

                return Vector2.one;
            }
            set
            {
                if (fillType == FillType.Texture)
                {
                    textureScale = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Radial)
                {

                    radial.size = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     Offset used by the texture and radial settings.
        /// </summary>
        public Vector2 Offset
        {
            get
            {
                if (fillType == FillType.Texture)
                    return textureOffset;
                else if (fillType == FillType.Radial)
                    return radial.offset;
                else if (fillType == FillType.Checkerboard)
                    return checkerboard.offset;
                else if (fillType == FillType.Stars)
                    return stars.offset;
                else if (fillType == FillType.PokaDots)
                    return pokaDots.offset;

                return Vector2.zero;
            }
            set
            {
                if (fillType == FillType.Texture)
                {
                    textureOffset = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Radial)
                {
                    radial.offset = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Checkerboard)
                {
                    checkerboard.offset = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Stars)
                {
                    stars.offset = value;
                    isDirty = true;
                }
                else if (fillType == FillType.PokaDots)
                {
                    pokaDots.offset = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     Thickness of lines used for stripes and radial.
        /// </summary>
        public float LineThickness
        {
            get
            {
                if (fillType == FillType.Stripes)
                    return stripe.lineWidth;
                else if (fillType == FillType.Radial)
                    return radial.lineThickness;

                return 0;
            }
            set
            {
                if (fillType == FillType.Stripes)
                {
                    stripe.lineWidth = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Radial)
                {
                    radial.lineThickness = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     Spacing between vertical lines.
        /// </summary>
        public float LineSpacing
        {
            get
            {
                if (fillType == FillType.Stripes)
                    return stripe.spacing;

                return 0;
            }
            set
            {
                if (fillType == FillType.Stripes)
                {
                    stripe.spacing = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     Offset of the vertically spaced lines.
        /// </summary>
        public float LineOffset
        {
            get
            {
                if(fillType == FillType.Stripes)
                    return stripe.offset;

                return 0;
            }
            set
            {
                if(fillType == FillType.Stripes)
                {
                    stripe.offset = value;
                    isDirty = true;
                }
            }
        }

        /// <summary>
        ///     The size of the comic fill elements.
        /// </summary>
        public float Size
        {
            get
            {
                if (fillType == FillType.Checkerboard)
                    return checkerboard.scale;
                else if (fillType == FillType.Stars)
                    return stars.scale;
                else if (fillType == FillType.PokaDots)
                    return pokaDots.scale;

                return 0;
            }
            set
            {
                if (fillType == FillType.Checkerboard)
                {
                    checkerboard.scale = value;
                    isDirty = true;
                }
                else if (fillType == FillType.Stars)
                {
                    stars.scale = value;
                    isDirty = true;
                }
                else if (fillType == FillType.PokaDots)
                {
                    pokaDots.scale = value;
                    isDirty = true;
                }
            }
        }

        #endregion

        private bool isDirty;
        private SpriteRenderer comic;
        private MaterialPropertyBlock props;

        #endregion

        #region ENGINE

        private void Start() => RenderPanel();

        private void OnValidate() => RenderPanel();

        private void LateUpdate()
        {
            if (isDirty && Application.isPlaying)
                RenderPanel();
        }

        public void RenderPanel()
        {
            try
            {
                comic = GetComponent<SpriteRenderer>();
                if(!comic.sharedMaterial.name.Contains("Comic Panel"))
                    comic.sharedMaterial = Resources.Load<Material>("Dynamic Comic Panels/Comic Panel");
                props = new MaterialPropertyBlock();
                comic.GetPropertyBlock(props);

                props.SetTexture("_MainTex", comic.sprite.texture);
                props.SetFloat("_Hue", hueShift);
                props.SetFloat("_Saturation", saturation);
                props.SetFloat("_Contrast", contrast);
                props.SetFloat("_Brightness", brightness);
                props.SetInt("_Invert", invert ? 1 : 0);

                if (fillType == FillType.Solid)
                    SetSolidColor();
                else if (fillType == FillType.Gradient)
                    SetGradientColor();
                else if (fillType == FillType.Texture)
                    SetTexture();
                else if (fillType == FillType.Stripes)
                    SetStripe();
                else if (fillType == FillType.Checkerboard)
                    SetCheckerboard();
                else if (fillType == FillType.Radial)
                    SetRadial();
                else if (fillType == FillType.Stars)
                    SetStars();
                else if (fillType == FillType.PokaDots)
                    SetPokaDots();
                else
                    SetSolidColor();

                comic.SetPropertyBlock(props);
                isDirty = false;
                OnComicRendered?.Invoke(fillType); //invoke
            } catch { }
        }

        private void SetSolidColor()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_SOLID);
            props.SetFloat("MUX", 0);
            props.SetColor("_Color", solidColor);
        }

        private void SetGradientColor()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_GRADIENT);
            props.SetFloat("MUX", 1);
            props.SetColor("_GradientColor1", gradientColor.color1);
            props.SetColor("_GradientColor2", gradientColor.color2);
            props.SetFloat("_GradientOffset", gradientColor.offset);
            props.SetFloat("_GradientRotation", gradientColor.rotation);
        }

        private void SetTexture()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_TEXTURE);
            props.SetFloat("MUX", 2);
            if (texture == null)
                texture = Resources.Load<Texture>("Shapes2D/1x1");
            props.SetTexture("_FillTexture", texture);
            props.SetColor("_Tint", solidColor);
            props.SetFloat("_TextureRotation", textureRotation);
            props.SetVector("_TextureScale", textureScale);
            props.SetVector("_TextureOffset", textureOffset);
        }

        private void SetStripe()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_STRIPE);
            props.SetFloat("MUX", 3);
            props.SetColor("_StripeColor1", stripe.color1);
            props.SetColor("_StripeColor2", stripe.color2);
            props.SetFloat("_StripeWidth", stripe.lineWidth);
            props.SetFloat("_StripeSpacing", stripe.spacing);
            props.SetFloat("_StripeRotation", stripe.rotation);
            props.SetFloat("_StripeOffset", stripe.offset);
        }

        private void SetCheckerboard()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_CHECKERBOARD);
            props.SetFloat("MUX", 4);
            props.SetColor("_CheckerboardColor1", checkerboard.color1);
            props.SetColor("_CheckerboardColor2", checkerboard.color2);
            props.SetFloat("_CheckerboardScale", checkerboard.scale);
            props.SetFloat("_CheckerboardRotation", checkerboard.rotation);
            props.SetVector("_CheckerboardOffset", checkerboard.offset);
        }

        private void SetRadial()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_A);
            //comic.material.EnableKeyword(KEYWORD_RADIAL);
            props.SetFloat("MUX", 5);
            props.SetColor("_RadialColor1", radial.color1);
            props.SetColor("_RadialColor2", radial.color2);
            props.SetFloat("_RadialThickness", radial.lineThickness);
            props.SetFloat("_RadialRotation", radial.rotation);
            props.SetVector("_RadialSize", radial.size);
            props.SetVector("_RadialOffset", radial.offset);
        }

        private void SetStars()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_B);
            //comic.material.EnableKeyword(KEYWORD_STARS);
            props.SetFloat("MUX", 6);
            props.SetColor("_StarColor1", stars.color1);
            props.SetColor("_StarColor2", stars.color2);
            props.SetFloat("_StarSize", stars.scale);
            props.SetFloat("_StarRotation", stars.rotation);
            props.SetVector("_StarOffset", stars.offset);

        }

        private void SetPokaDots()
        {
            //comic.material.EnableKeyword(KEYWORD_SELECTOR_B);
            //comic.material.EnableKeyword(KEYWORD_POKA_DOTS);
            props.SetFloat("MUX", 7);
            props.SetColor("_PokaDotColor1", pokaDots.color1);
            props.SetColor("_PokaDotColor2", pokaDots.color2);
            props.SetFloat("_PokaDotSize", pokaDots.scale);
            props.SetFloat("_PokaDotRotation", pokaDots.rotation);
            props.SetVector("_PokaDotOffset", pokaDots.offset);

        }

        #endregion
    }
}