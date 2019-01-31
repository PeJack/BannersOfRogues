using UnityEngine;
using UnityEngine.UI;
using BannersOfRogues.Systems.Utils;

namespace BannersOfRogues.UI {
    public class Renderer : PooledObject {
        [SerializeField] GameObject backgroundObject;
        [SerializeField] GameObject textObject;

        public bool IsAscii { get; set; }

        public bool Active {
            set
            {
                if (IsAscii) {
                    backgroundObject.SetActive(value);
                    textObject.SetActive(value);
                } else {
                    backgroundObject.SetActive(value);
                    textObject.SetActive(false);
                }
            }
        }

        public Sprite BackgroundImage {
            get { return backgroundObject.GetComponent<SpriteRenderer>().sprite; }
            set { backgroundObject.GetComponent<SpriteRenderer>().sprite = value; }
        }

        public float BackgroundAlpha {
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;

                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(backgroundObject.GetComponent<SpriteRenderer>().color.r,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.g,
                                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.b,
                                                                                  value);
            }

            get
            {
                return backgroundObject.GetComponent<SpriteRenderer>().color.a;
            }
        }

        public int BackgroundAlphaInt {
            set
            {
                if (value < 0) value = 0;
                if (value > 255) value = 255;

                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(backgroundObject.GetComponent<SpriteRenderer>().color.r,
                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.g,
                                                                  backgroundObject.GetComponent<SpriteRenderer>().color.b,
                                                                  value / 255);
            }

            get
            {
                return (int)(backgroundObject.GetComponent<SpriteRenderer>().color.a * 255);
            }
        }
        public Color BackgroundColor {
            set
            {
                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, backgroundObject.GetComponent<SpriteRenderer>().color.a);
            }
        }

        public Color BackgroundColorWithAlpha {
            get
            {
                return backgroundObject.GetComponent<SpriteRenderer>().color;
            }

            set
            {
                backgroundObject.GetComponent<SpriteRenderer>().color = new Color(value.r, value.g, value.b, value.a);
            }
        }

        public char Text {
            get
            {
                string val = textObject.GetComponent<Text>().text;
                char result = string.IsNullOrEmpty(val) ? ' ' : val[0];
                return result;
            }

            set
            {
                string val = "";
                val += value;
                textObject.GetComponent<Text>().text = val;
            }
        }

        public float TextAlpha {
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;

                textObject.GetComponent<Text>().color = new Color(textObject.GetComponent<Text>().color.r,
                                                                  textObject.GetComponent<Text>().color.g,
                                                                  textObject.GetComponent<Text>().color.b,
                                                                  value);
            }

            get
            {
                return textObject.GetComponent<Text>().color.a;
            }
        }

        public int TextAlphaInt {
            set
            {
                if (value < 0) value = 0;
                if (value > 255) value = 255;

                textObject.GetComponent<Text>().color = new Color(textObject.GetComponent<Text>().color.r,
                                                                  textObject.GetComponent<Text>().color.g,
                                                                  textObject.GetComponent<Text>().color.b,
                                                                  value / 255);
            }
            get
            {
                return (int)(textObject.GetComponent<Text>().color.a * 255);
            }
        }

        public Color TextColor {
            set
            {
                textObject.GetComponent<Text>().color = new Color(value.r, value.g, value.b, textObject.GetComponent<Text>().color.a);
            }
        }

        public Color TextColorWithAlpha {
            get
            {
                return textObject.GetComponent<Text>().color;
            }
            set
            {
                textObject.GetComponent<Text>().color = new Color(value.r, value.g, value.b, value.a);
            }
        }
    }
}
