/*
 * KropParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * This program is free software: you can redistribute it and/or
 * modify it under the terms of the BSD license.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * LICENSE.txt file for more details.
 *
 * Copyright (c) 2018 Stuart Gueissaz. All rights reserved.
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace Krop.KropGrammaticaParser {

    /**
     * <remarks>A token stream parser.</remarks>
     */
    internal class KropParser : RecursiveDescentParser {

        /**
         * <summary>An enumeration with the generated production node
         * identity constants.</summary>
         */
        private enum SynteticPatterns {
            SUBPRODUCTION_1 = 3001,
            SUBPRODUCTION_2 = 3002
        }

        /**
         * <summary>Creates a new parser with a default analyzer.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public KropParser(TextReader input)
            : base(input) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new parser.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <param name='analyzer'>the analyzer to parse with</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public KropParser(TextReader input, KropAnalyzer analyzer)
            : base(input, analyzer) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new tokenizer for this parser. Can be overridden
         * by a subclass to provide a custom implementation.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <returns>the tokenizer created</returns>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        protected override Tokenizer NewTokenizer(TextReader input) {
            return new KropTokenizer(input);
        }

        /**
         * <summary>Initializes the parser by creating all the production
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            ProductionPattern             pattern;
            ProductionPatternAlternative  alt;

            pattern = new ProductionPattern((int) KropConstants.PROGRAM,
                                            "program");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.DECLARATION_STATEMENT, 0, -1);
            alt.AddProduction((int) KropConstants.STATEMENT, 1, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.DECLARATION_STATEMENT,
                                            "declarationStatement");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.INT_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.STATEMENT,
                                            "statement");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.INSTRUCTION_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.IF_ELSE_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.WHILE_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.DIRE_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.SET_VAR_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.INSTRUCTION_STATEMENT,
                                            "instructionStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.INSTRUCTION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.SEMICOLON, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.IF_ELSE_STATEMENT,
                                            "ifElseStatement");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.IF_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.ELSE_STATEMENT, 0, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.IF_STATEMENT,
                                            "ifStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.IF, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.CONDITON_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.LEFT_BRACE, 1, 1);
            alt.AddProduction((int) KropConstants.PROGRAM, 1, 1);
            alt.AddToken((int) KropConstants.RIGHT_BRACE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.ELSE_STATEMENT,
                                            "elseStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.ELSE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.LEFT_BRACE, 1, 1);
            alt.AddProduction((int) KropConstants.PROGRAM, 1, 1);
            alt.AddToken((int) KropConstants.RIGHT_BRACE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.WHILE_STATEMENT,
                                            "whileStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.WHILE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.CONDITON_STATEMENT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.LEFT_BRACE, 1, 1);
            alt.AddProduction((int) KropConstants.PROGRAM, 1, 1);
            alt.AddToken((int) KropConstants.RIGHT_BRACE, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.DIRE_STATEMENT,
                                            "direStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.DIRE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.DIRE_VALUE, 1, 1);
            alt.AddToken((int) KropConstants.SEMICOLON, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.INT_STATEMENT,
                                            "intStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.INT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 1, -1);
            alt.AddToken((int) KropConstants.WORD, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.EGAL, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.SEMICOLON, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.SET_VAR_STATEMENT,
                                            "setVarStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.WORD, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.EGAL, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.SET_VAR_VALUE, 1, 1);
            alt.AddToken((int) KropConstants.SEMICOLON, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.CONDITON_STATEMENT,
                                            "conditonStatement");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.CONDITION_EXPR, 1, 1);
            alt.AddToken((int) KropConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.CONDITION_EXPR,
                                            "conditionExpr");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_1, 0, 1);
            alt.AddProduction((int) KropConstants.CONDITION_PARAMETER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.CONDITION_PARAMETER,
                                            "conditionParameter");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.CONDITION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.TRUE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.FALSE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.BOOLEAN_EXPRESSION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.BOOLEAN_EXPRESSION,
                                            "booleanExpression");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.BOOLEAN_EXPRESSION_REST, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.BOOLEAN_EXPRESSION_REST,
                                            "booleanExpressionRest");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.EGAL, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.BIGGER, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.SMALLER, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.SET_VAR_VALUE,
                                            "setVarValue");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.STRING_VALUE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.DIRE_VALUE,
                                            "direValue");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.STRING_VALUE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.ATOM, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.STRING_VALUE,
                                            "stringValue");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.APOSTROPHE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.SENTENCE, 1, 1);
            alt.AddToken((int) KropConstants.APOSTROPHE, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.SENTENCE,
                                            "sentence");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_2, 1, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.EXPRESSION,
                                            "expression");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.TERM, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION_REST, 0, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.EXPRESSION_REST,
                                            "expressionRest");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.ADD, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.SUB, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.TERM,
                                            "term");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.FACTOR, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.TERM_REST, 0, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.TERM_REST,
                                            "termRest");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.MUL, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.TERM, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.DIV, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.TERM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.FACTOR,
                                            "factor");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.ATOM, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddProduction((int) KropConstants.EXPRESSION, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            alt.AddToken((int) KropConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) KropConstants.ATOM,
                                            "atom");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.SUB, 0, 1);
            alt.AddToken((int) KropConstants.NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.WORD, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_1,
                                            "Subproduction1");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) KropConstants.NOT, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_2,
                                            "Subproduction2");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) KropConstants.ATOM, 1, 1);
            alt.AddToken((int) KropConstants.SPACE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);
        }
    }
}
