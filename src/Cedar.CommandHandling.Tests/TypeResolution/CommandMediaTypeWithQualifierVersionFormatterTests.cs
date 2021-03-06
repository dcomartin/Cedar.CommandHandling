﻿namespace Cedar.CommandHandling.TypeResolution
{
    using Cedar.CommandHandling.Http.TypeResolution;
    using FluentAssertions;
    using Xunit;

    public class CommandMediaTypeWithQualifierVersionFormatterTests
    {
        private readonly CommandMediaTypeWithQualifierVersionFormatter _sut;

        public CommandMediaTypeWithQualifierVersionFormatterTests()
        {
            _sut = new CommandMediaTypeWithQualifierVersionFormatter();
        }

        [Fact]
        public void Can_parse_unversioned_media_type()
        {
            var parsedMediaType = _sut.Parse("application/vnd.command+json");

            parsedMediaType.CommandName.Should().Be("command");
            parsedMediaType.Version.Should().Be(null);
            parsedMediaType.SerializationType.Should().Be("json");
        }

        [Fact]
        public void Can_generate_unversioned_media_type()
        {
            var formatter = new CommandMediaTypeWithDotVersionFormatter();

            string mediaType = formatter.GetMediaType(new CommandNameAndVersion("command"), "xml");

            mediaType.Should().Be("application/vnd.command+xml");
        }

        [Fact]
        public void Can_parse_versioned_media_type()
        {
            var parsedMediaType = _sut.Parse("application/vnd.command+json;v=2");

            parsedMediaType.CommandName.Should().Be("command");
            parsedMediaType.Version.Should().Be(2);
            parsedMediaType.SerializationType.Should().Be("json");
        }

        [Fact]
        public void Can_generate_versioned_media_type()
        {
            string mediaType = _sut.GetMediaType(new CommandNameAndVersion("command", 2), "xml");

            mediaType.Should().Be("application/vnd.command+xml;v=2");
        }
    }
}