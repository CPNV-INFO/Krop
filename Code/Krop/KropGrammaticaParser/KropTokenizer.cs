/*
 * KropTokenizer.cs
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
     * <remarks>A character stream tokenizer.</remarks>
     */
    internal class KropTokenizer : Tokenizer {

        /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public KropTokenizer(TextReader input)
            : base(input, true) {

            CreatePatterns();
        }

        /**
         * <summary>Initializes the tokenizer by creating all the token
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            TokenPattern  pattern;

            pattern = new TokenPattern((int) KropConstants.IF,
                                       "IF",
                                       TokenPattern.PatternType.STRING,
                                       "IF");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.ELSE,
                                       "ELSE",
                                       TokenPattern.PatternType.STRING,
                                       "ELSE");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.WHILE,
                                       "WHILE",
                                       TokenPattern.PatternType.STRING,
                                       "WHILE");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.DIRE,
                                       "DIRE",
                                       TokenPattern.PatternType.STRING,
                                       "DIRE");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.NOT,
                                       "NOT",
                                       TokenPattern.PatternType.STRING,
                                       "NOT");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.TRUE,
                                       "TRUE",
                                       TokenPattern.PatternType.STRING,
                                       "TRUE");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.FALSE,
                                       "FALSE",
                                       TokenPattern.PatternType.STRING,
                                       "FALSE");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.INT,
                                       "INT",
                                       TokenPattern.PatternType.STRING,
                                       "INT");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.STRING,
                                       "STRING",
                                       TokenPattern.PatternType.STRING,
                                       "STRING");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.AND,
                                       "AND",
                                       TokenPattern.PatternType.STRING,
                                       "AND");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.OR,
                                       "OR",
                                       TokenPattern.PatternType.STRING,
                                       "OR");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.CONDITION,
                                       "CONDITION",
                                       TokenPattern.PatternType.REGEXP,
                                       "SurUnePheromone|ObstacleEnFace|ObstacleADroite|ObstacleAGauche");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.INSTRUCTION,
                                       "INSTRUCTION",
                                       TokenPattern.PatternType.REGEXP,
                                       "PoserPheromone|PrendrePheromone|Avancer|TournerADroite|TournerAGauche");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.INPUT,
                                       "INPUT",
                                       TokenPattern.PatternType.STRING,
                                       "INPUT");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.EGAL,
                                       "EGAL",
                                       TokenPattern.PatternType.STRING,
                                       "=");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.BIGGER,
                                       "BIGGER",
                                       TokenPattern.PatternType.STRING,
                                       ">");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.SMALLER,
                                       "SMALLER",
                                       TokenPattern.PatternType.STRING,
                                       "<");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.ADD,
                                       "ADD",
                                       TokenPattern.PatternType.STRING,
                                       "+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.SUB,
                                       "SUB",
                                       TokenPattern.PatternType.STRING,
                                       "-");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.MUL,
                                       "MUL",
                                       TokenPattern.PatternType.STRING,
                                       "*");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.DIV,
                                       "DIV",
                                       TokenPattern.PatternType.STRING,
                                       "/");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.LEFT_PAREN,
                                       "LEFT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       "(");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.RIGHT_PAREN,
                                       "RIGHT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       ")");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.LEFT_BRACE,
                                       "LEFT_BRACE",
                                       TokenPattern.PatternType.STRING,
                                       "{");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.RIGHT_BRACE,
                                       "RIGHT_BRACE",
                                       TokenPattern.PatternType.STRING,
                                       "}");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.SEMICOLON,
                                       "SEMICOLON",
                                       TokenPattern.PatternType.STRING,
                                       ";");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.APOSTROPHE,
                                       "APOSTROPHE",
                                       TokenPattern.PatternType.STRING,
                                       "'");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.SPACE,
                                       "SPACE",
                                       TokenPattern.PatternType.STRING,
                                       " ");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.EXCLAMATION,
                                       "EXCLAMATION",
                                       TokenPattern.PatternType.STRING,
                                       "!");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.QUESTION_MARK,
                                       "QUESTION_MARK",
                                       TokenPattern.PatternType.STRING,
                                       "?");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.COLON,
                                       "COLON",
                                       TokenPattern.PatternType.STRING,
                                       ":");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.COMMA,
                                       "COMMA",
                                       TokenPattern.PatternType.STRING,
                                       ",");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.DOT,
                                       "DOT",
                                       TokenPattern.PatternType.STRING,
                                       ".");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.NUMBER,
                                       "NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.WORD,
                                       "WORD",
                                       TokenPattern.PatternType.REGEXP,
                                       "[a-z]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[\\t\\n\\r]+");
            pattern.Ignore = true;
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.BACKSLASH_APOSTROPHE,
                                       "BACKSLASH_APOSTROPHE",
                                       TokenPattern.PatternType.STRING,
                                       "\\'");
            AddPattern(pattern);

            pattern = new TokenPattern((int) KropConstants.QUOTE,
                                       "QUOTE",
                                       TokenPattern.PatternType.REGEXP,
                                       "\"");
            AddPattern(pattern);
        }
    }
}
