﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AntlrCSharp;
using Antlr4.Runtime;

namespace AntlrCSharpTests
{
    [TestClass]
    public class ParserTest
    {
        private SpeakParser Setup(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            SpeakLexer speakLexer = new SpeakLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            SpeakParser speakParser = new SpeakParser(commonTokenStream);

            return speakParser;   
        }

        [TestMethod]
        public void TestChat()
        {
            SpeakParser parser = Setup("john says \"hello\" \n michael says \"world\" \n");

            SpeakParser.ChatContext context = parser.chat();
            SpeakVisitor visitor = new SpeakVisitor();
            visitor.Visit(context);

            Assert.AreEqual(2, visitor.Lines.Count);
        }

        [TestMethod]
        public void TestLine()
        {
            SpeakParser parser = Setup("john says \"hello\" \n");

            SpeakParser.LineContext context = parser.line();
            SpeakVisitor visitor = new SpeakVisitor();
            SpeakLine line = (SpeakLine) visitor.VisitLine(context);     
            
            Assert.AreEqual("john", line.Person);
            Assert.AreEqual("hello", line.Text);
        }

        [TestMethod]
        public void TestWrongLine()
        {
            SpeakParser parser = Setup("john sayan \"hello\" \n");

            var context = parser.line();
            
            Assert.IsInstanceOfType(context, typeof(SpeakParser.LineContext));            
            Assert.AreEqual("john", context.name().GetText());
            Assert.IsNull(context.SAYS());
            Assert.AreEqual("johnsayan\"hello\"\n", context.GetText());
        }

        private TSqlParser Setup2(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            var speakLexer = new TSqlLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            var speakParser = new TSqlParser(commonTokenStream);

            return speakParser;
        }

        [TestMethod]
        public void boop()
        {
            TSqlParser p = Setup2("SELECT A FROM B");
            var c = p.tsql_file();
            c.
        }
    }
}
