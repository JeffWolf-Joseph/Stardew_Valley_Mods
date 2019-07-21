using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StardustCore.UIUtilities.SpriteFonts.Components
{
    public class TexturedString
    {
        public List<TexturedCharacter> characters;
        public Vector2 position;
        public string label;
        public float scale;

        public TexturedString(string Label, Vector2 Position, List<TexturedCharacter> Characters, bool useRightPadding = true,float Scale=1f)
        {
            this.label = Label;
            this.characters = Characters;
            this.position = Position;
            this.scale = Scale;
            this.setCharacterPositions(useRightPadding);
        }

        /// <summary>Sets the character positions relative to the string's position on screen.</summary>
        public void setCharacterPositions(bool useRightPadding = true)
        {
            int index = 0;
            TexturedCharacter lastSeenChar = new TexturedCharacter();
            foreach (var c in this.characters)
            {
                if (index == 0)
                    c.position = new Vector2(this.position.X + (c.spacing.LeftPadding*this.scale), this.position.Y);
                else if (useRightPadding)
                    c.position = new Vector2(this.position.X + (c.spacing.LeftPadding*this.scale) + (lastSeenChar.spacing.RightPadding*this.scale) + (lastSeenChar.texture.Width*this.scale * index), this.position.Y);
                else
                    c.position = new Vector2(this.position.X + (c.spacing.LeftPadding*this.scale) + (lastSeenChar.texture.Width*this.scale * index), this.position.Y);
                //StardustCore.ModCore.ModMonitor.Log(c.character.ToString());
                //StardustCore.ModCore.ModMonitor.Log(c.position.ToString());
                lastSeenChar = c;
                index++;
            }
        }

        /// <summary>
        /// Sets the position of the textured string and all characters accordingly.
        /// </summary>
        /// <param name="pos"></param>
        public void setPosition(Vector2 pos)
        {
            this.position = pos;
            this.setCharacterPositions();
        }

        /// <summary>Adds a textured character to a textured string.</summary>
        public void addCharacterToEnd(TexturedCharacter ch, bool useRightPadding = true)
        {
            this.characters.Add(ch);
            this.setCharacterPositions(useRightPadding);
        }

        /// <summary>Adds a list of textured characters to a textured string.</summary>
        public void addCharactersToEnd(List<TexturedCharacter> chList, bool useRightPadding = true)
        {
            foreach (var ch in chList)
                this.characters.Add(ch);
            this.setCharacterPositions(useRightPadding);
        }

        /// <summary>Adds the strings together and allows the position to be set.</summary>
        public TexturedString addStrings(TexturedString first, TexturedString second, Vector2 NewPosition, bool useRightPadding = true)
        {
            var newString = first + second;
            newString.position = NewPosition;
            newString.setCharacterPositions(useRightPadding);
            return newString;
        }

        /// <summary>Operator overload of +. Adds the two strings together and sets a new 0,0 position.</summary>
        public static TexturedString operator +(TexturedString first, TexturedString second)
        {
            List<TexturedCharacter> characterList = new List<TexturedCharacter>();
            foreach (var v in first.characters)
                characterList.Add(v);
            foreach (var v in second.characters)
                characterList.Add(v);
            TexturedString newString = new TexturedString("", new Vector2(0, 0), characterList);
            return newString;
        }

        /// <summary>Removes the characters from the textured word.</summary>
        public void removeCharactersFromEnd(int index, int howMany)
        {
            this.characters.RemoveRange(index, howMany);
        }

        /// <summary>Draw the textured string.</summary>
        public void draw(SpriteBatch b)
        {
            foreach (var v in this.characters)
                v.draw(b);
        }

        /// <summary>
        /// Draw the textured string.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="Scale"></param>
        /// <param name="Depth"></param>
        public void draw(SpriteBatch b, Rectangle sourceRectangle, float Scale, float Depth)
        {
            foreach (var v in this.characters)
                v.draw(b,sourceRectangle,Scale,Depth);
        }
        /// <summary>
        /// Draw the textured string.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="Depth"></param>
        public void draw(SpriteBatch b, Rectangle sourceRectangle, float Depth)
        {
            foreach (var v in this.characters)
                v.draw(b, sourceRectangle, this.scale, Depth);
        }

        /// <summary>Returns a copy of this object.</summary>
        public TexturedString copy()
        {
            return new TexturedString(this.label, this.position, this.characters);
        }

        /// <summary>Returns a copy of this object at the specified position.</summary>
        public TexturedString copy(Vector2 newPosition)
        {
            return new TexturedString(this.label, newPosition, this.characters);
        }

        /// <summary>Returns a new textured strings with a different label and position.</summary>
        public TexturedString copy(string label, Vector2 newPosition)
        {
            return new TexturedString(label, newPosition, this.characters);
        }
    }
}
