/*
 * This class defines what each level contains
 * accroding to the level info, generate inputs and distractor option
 */


using System;


namespace test
{
    public class Level
    {
        private char[] potentialOperators;
        private int[] numberRange;
        /* numberRange[0] means lower
         * numberRange[1] means upper
        */ 
        private int numOfInputs;

        public char[] getPotentialOperators()
        {
            return potentialOperators;
        }

        public int[] getNumberRange()
        {
            return numberRange;
        }

        public int getnumOfInputs()
        {
            return numOfInputs;
        }

        public Level()
        {
            potentialOperators = new char[] { '+', '-', '*', '/' };
            numberRange = new int[2];
            numOfInputs = potentialOperators.Length + 1;
        }

        public Level(int level)
        {
            numberRange = new int[2];
            switch (level)
            {
                case 1:
                    /* plus with 2 inputs
                     * like: [ ] + [ ] = 10
                    */ 
                    potentialOperators = new char[] { '+' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 2:
                    /* minus with 2 inputs
                     * like: [ ] - 2 = [ ]
                    */ 
                    potentialOperators = new char[] { '-' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 3:
                    /* plus twice with 2 inputs
                     * like: [ ] + 4 + [ ] = 16
                    */
                    potentialOperators = new char[] { '+', '+' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 4:
                    /* minus twice with 2 inputs
                    * like [ ] - 2 - [ ] = 0
                    */ 
                    potentialOperators = new char[] { '-', '-' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 5:
                    /* plus twice with 3 inputs
                     * like [ ] + 2 + [ ] = [ ]
                    */ 
                    potentialOperators = new char[] { '+', '+' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 6:
                    /* minus twice with 3 inputs
                     * like [ ] - [ ] - 3 = [ ]
                    */
                    potentialOperators = new char[] { '-', '-' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 7:
                    /* plus and minus with 2 inputs
                     * like [ ] + 5 - [ ] = 9
                    */
                    potentialOperators = new char[] { '+', '-' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 8:
                    /* plus and minus with 3 inputs
                     * like [ ] + [ ] - 3 = [ ]
                     */
                    potentialOperators = new char[] { '+', '-' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 9:
                    /* times with 2 inputs
                     * like [ ] x [ ] = 12 
                     */
                    potentialOperators = new char[] { '*' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 10:
                    /* division with 2 inputs
                     * like [ ] ÷ [ ] = 2
                     */
                    potentialOperators = new char[] { '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 11:
                    /* times twice with 2 inputs
                     * like [ ] x 3 x [ ] = 24
                     */
                    potentialOperators = new char[] { '*', '*' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 12:
                    /* division twice with 2 inputs and no decimals allowed
                     * like 18 ÷ [ ] ÷ [ ] = 3
                     */
                    potentialOperators = new char[] { '/', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 13:
                    /* times twice with 3 inputs
                     * like [ ] x 2 x [ ] = [ ]
                     */
                    potentialOperators = new char[] { '*', '*' };
                    numberRange[0] = 0;
                    numberRange[1] = 30;
                    numOfInputs = 3;
                    break;
                case 14:
                    /* division twice with 3 inputs
                     * like 24 ÷ [ ] ÷ [ ] = [ ]
                     */
                    potentialOperators = new char[] { '/', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 15:
                    /* Mixed operation with 1 input
                     * like 6 - [ ] ÷ 2 = 2
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 1;
                    break;
                case 16:
                    /* Mixed operation with 2 inputs
                     * like  2 * [ ] + [ ] = 11
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 2;
                    break;
                case 17:
                    /* Mixed operation with 3 input
                     * like 4 * [ ] - [ ] = [ ]
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 18:
                    /* Mixed operation with 3 input
                     * like 3 * [ ] - [ ] ÷ [ ] = 11
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 3;
                    break;
                case 19:
                    /* Mixed operation with 4 input
                     * like 4 + [ ] - [ ] ÷ [ ] = [ ]
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 4;
                    break;
                case 20:
                    /* Mixed operation with 5 input
                     * like [ ] * [ ] - [ ] ÷ [ ] = [ ]
                     */
                    potentialOperators = new char[] { '+', '-', '*', '/' };
                    numberRange[0] = 0;
                    numberRange[1] = 10;
                    numOfInputs = 5;
                    break;
            }
        }
    }
}
