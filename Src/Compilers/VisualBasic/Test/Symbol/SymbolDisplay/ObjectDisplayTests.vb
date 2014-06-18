﻿Imports System.Globalization
Imports System.Threading.Thread
Imports Roslyn.Test.Utilities
Imports Xunit

Namespace Microsoft.CodeAnalysis.VisualBasic.UnitTests

    Public Class ObjectDisplayTests

        <Fact()>
        Public Sub IntegralPrimitives()
            Assert.Equal("1", FormatPrimitive(CByte(1)))
            Assert.Equal("123", FormatPrimitive(CByte(123)))
            Assert.Equal("255", FormatPrimitive(Byte.MaxValue))
            Assert.Equal("1", FormatPrimitive(CSByte(1)))
            Assert.Equal("123", FormatPrimitive(CSByte(123)))
            Assert.Equal("-1", FormatPrimitive(CSByte(-1)))
            Assert.Equal("1", FormatPrimitive(CUShort(1)))
            Assert.Equal("123", FormatPrimitive(CUShort(123)))
            Assert.Equal("65535", FormatPrimitive(UShort.MaxValue))
            Assert.Equal("1", FormatPrimitive(CShort(1)))
            Assert.Equal("123", FormatPrimitive(CShort(123)))
            Assert.Equal("-1", FormatPrimitive(CShort(-1)))
            Assert.Equal("1", FormatPrimitive(CUInt(1)))
            Assert.Equal("123", FormatPrimitive(CUInt(123)))
            Assert.Equal("4294967295", FormatPrimitive(UInteger.MaxValue))
            Assert.Equal("1", FormatPrimitive(CInt(1)))
            Assert.Equal("123", FormatPrimitive(CInt(123)))
            Assert.Equal("-1", FormatPrimitive(CInt(-1)))
            Assert.Equal("1", FormatPrimitive(CULng(1)))
            Assert.Equal("123", FormatPrimitive(CULng(123)))
            Assert.Equal("18446744073709551615", FormatPrimitive(ULong.MaxValue))
            Assert.Equal("1", FormatPrimitive(CLng(1)))
            Assert.Equal("123", FormatPrimitive(CLng(123)))
            Assert.Equal("-1", FormatPrimitive(CLng(-1)))

            ' Dev10 EE does not "pad" positive values with '0', but this is desired for Roslyn
            Assert.Equal("&H00", FormatPrimitiveUsingHexadecimalNumbers(CByte(0)))
            Assert.Equal("&H01", FormatPrimitiveUsingHexadecimalNumbers(CByte(1)))
            Assert.Equal("&H7F", FormatPrimitiveUsingHexadecimalNumbers(CByte(&H7F)))
            Assert.Equal("&HFF", FormatPrimitiveUsingHexadecimalNumbers(Byte.MaxValue))
            Assert.Equal("&H00", FormatPrimitiveUsingHexadecimalNumbers(CSByte(0)))
            Assert.Equal("&H01", FormatPrimitiveUsingHexadecimalNumbers(CSByte(1)))
            Assert.Equal("&H7F", FormatPrimitiveUsingHexadecimalNumbers(CSByte(&H7F)))
            Assert.Equal("&HFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(CSByte(-1)))
            Assert.Equal("&HFFFFFFFE", FormatPrimitiveUsingHexadecimalNumbers(CSByte((-2))))
            Assert.Equal("&H0000", FormatPrimitiveUsingHexadecimalNumbers(CUShort(0)))
            Assert.Equal("&H0001", FormatPrimitiveUsingHexadecimalNumbers(CUShort(1)))
            Assert.Equal("&H007F", FormatPrimitiveUsingHexadecimalNumbers(CUShort(&H7F)))
            Assert.Equal("&HFFFF", FormatPrimitiveUsingHexadecimalNumbers(UShort.MaxValue))
            Assert.Equal("&H0000", FormatPrimitiveUsingHexadecimalNumbers(CShort(0)))
            Assert.Equal("&H0001", FormatPrimitiveUsingHexadecimalNumbers(CShort(1)))
            Assert.Equal("&H007F", FormatPrimitiveUsingHexadecimalNumbers(CShort(&H7F)))
            Assert.Equal("&HFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(CShort(-1)))
            Assert.Equal("&HFFFFFFFE", FormatPrimitiveUsingHexadecimalNumbers(CShort((-2))))
            Assert.Equal("&H00000000", FormatPrimitiveUsingHexadecimalNumbers(CUInt(0)))
            Assert.Equal("&H00000001", FormatPrimitiveUsingHexadecimalNumbers(CUInt(1)))
            Assert.Equal("&H0000007F", FormatPrimitiveUsingHexadecimalNumbers(CUInt(&H7F)))
            Assert.Equal("&HFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(UInteger.MaxValue))
            Assert.Equal("&H00000000", FormatPrimitiveUsingHexadecimalNumbers(CInt(0)))
            Assert.Equal("&H00000001", FormatPrimitiveUsingHexadecimalNumbers(CInt(1)))
            Assert.Equal("&H0000007F", FormatPrimitiveUsingHexadecimalNumbers(CInt(&H7F)))
            Assert.Equal("&HFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(CInt(-1)))
            Assert.Equal("&HFFFFFFFE", FormatPrimitiveUsingHexadecimalNumbers(CInt((-2))))
            Assert.Equal("&H0000000000000000", FormatPrimitiveUsingHexadecimalNumbers(CULng(0)))
            Assert.Equal("&H0000000000000001", FormatPrimitiveUsingHexadecimalNumbers(CULng(1)))
            Assert.Equal("&H000000000000007F", FormatPrimitiveUsingHexadecimalNumbers(CULng(&H7F)))
            Assert.Equal("&HFFFFFFFFFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(ULong.MaxValue))
            Assert.Equal("&H0000000000000000", FormatPrimitiveUsingHexadecimalNumbers(CLng(0)))
            Assert.Equal("&H0000000000000001", FormatPrimitiveUsingHexadecimalNumbers(CLng(1)))
            Assert.Equal("&H000000000000007F", FormatPrimitiveUsingHexadecimalNumbers(CLng(&H7F)))
            Assert.Equal("&HFFFFFFFFFFFFFFFF", FormatPrimitiveUsingHexadecimalNumbers(CLng(-1)))
            Assert.Equal("&HFFFFFFFFFFFFFFFE", FormatPrimitiveUsingHexadecimalNumbers(CLng((-2))))
        End Sub

        <Fact>
        Public Sub Booleans()
            Assert.Equal("True", FormatPrimitive(True))
            Assert.Equal("False", FormatPrimitive(False))
        End Sub

        <Fact>
        Public Sub NothingLiterals()
            Assert.Equal("Nothing", FormatPrimitive(Nothing))
        End Sub

        <Fact>
        Public Sub Decimals()
            Assert.Equal("2", FormatPrimitive(CType(2, Decimal)))
        End Sub

        <Fact>
        Public Sub Singles()
            Assert.Equal("2", FormatPrimitive(CType(2, Single)))
        End Sub

        <Fact>
        Public Sub Doubles()
            Assert.Equal("2", FormatPrimitive(CType(2, Double)))
        End Sub

        <Fact>
        Public Sub Characters()
            ' Note - this is significanlty different from what Dev10 VB EE does, which is
            ' - ignore "nq" setting
            ' - print non-printable characters, no escaping
            Assert.Equal("""x""c", FormatPrimitive("x"c, quoteStrings:=True))
            Assert.Equal("x", FormatPrimitive("x"c, quoteStrings:=False))
            Assert.Equal("""x""c", FormatPrimitiveUsingHexadecimalNumbers("x"c, quoteStrings:=True))
            Assert.Equal("x", FormatPrimitiveUsingHexadecimalNumbers("x"c, quoteStrings:=False))

            Assert.Equal("vbNullChar", FormatPrimitiveUsingHexadecimalNumbers(ChrW(0), quoteStrings:=True))
            Assert.Equal("ChrW(&H1E)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&H1E), quoteStrings:=True))
            Assert.Equal("ChrW(&H1E)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&H1E), quoteStrings:=False))
            Assert.Equal("ChrW(20)", FormatPrimitive(ChrW(20)))
            Assert.Equal("vbBack", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&H8), quoteStrings:=True))
            Assert.Equal("vbBack", FormatPrimitive(ChrW(&H8)))
            Assert.Equal("vbLf", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HA), quoteStrings:=True))
            Assert.Equal("vbVerticalTab", FormatPrimitiveUsingHexadecimalNumbers(vbVerticalTab(0), quoteStrings:=True))
            Assert.Equal("vbTab", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&H9), quoteStrings:=True))
            Assert.Equal("vbFormFeed", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HC), quoteStrings:=True))
            Assert.Equal("vbCr", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HD), quoteStrings:=True))
        End Sub

        <Fact()>
        Public Sub Strings()
            Assert.Equal("", FormatPrimitive("", quoteStrings:=False))
            Assert.Equal("a", FormatPrimitive("a", quoteStrings:=False))
            Assert.Equal("""", FormatPrimitive("""", quoteStrings:=False))
            Assert.Equal("""""", FormatPrimitive("", quoteStrings:=True))
            Assert.Equal("""""""""", FormatPrimitive("""", quoteStrings:=True))

            Assert.Equal("ChrW(&HABCD)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HABCD), quoteStrings:=True))
            Assert.Equal("ChrW(43981)", FormatPrimitive(ChrW(&HABCD), quoteStrings:=True))
            Assert.Equal("ChrW(&HABCD)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HABCD), quoteStrings:=False))
            Assert.Equal("ChrW(43981)", FormatPrimitive(ChrW(&HABCD), quoteStrings:=False))

            Dim s = "a" & ChrW(&HABCF) & ChrW(&HABCD) & vbCrLf & "b"

            Assert.Equal("""a"" & ChrW(&HABCD)", FormatPrimitiveUsingHexadecimalNumbers("a" & ChrW(&HABCD), quoteStrings:=True))
            Assert.Equal("ChrW(&HABCD) & ""a""", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HABCD) & "a", quoteStrings:=True))
            Assert.Equal("""a"" & ChrW(&HABCD) & ""a""", FormatPrimitiveUsingHexadecimalNumbers("a" & ChrW(&HABCD) & "a", quoteStrings:=True))
            Assert.Equal("ChrW(&HABCF) & ChrW(&HABCD)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HABCF) & ChrW(&HABCD), quoteStrings:=True))
            Assert.Equal("ChrW(&HABCF) & ""a"" & ChrW(&HABCD)", FormatPrimitiveUsingHexadecimalNumbers(ChrW(&HABCF) & "a" & ChrW(&HABCD), quoteStrings:=True))
            Assert.Equal("""a"" & ChrW(&HABCF) & ChrW(&HABCD) & vbCrLf & ""b""", FormatPrimitiveUsingHexadecimalNumbers(s, quoteStrings:=True))

            ' non-printable characters are replaced by spaces if quoting is disabled
            Assert.Equal(s, FormatPrimitiveUsingHexadecimalNumbers(s, quoteStrings:=False))
            Assert.Equal("a....b", ObjectDisplay.FormatLiteral(s, quote:=False, nonPrintableSubstitute:="."c, useHexadecimalNumbers:=False))
            Assert.Equal("""a....b""", ObjectDisplay.FormatLiteral(s, quote:=True, nonPrintableSubstitute:="."c, useHexadecimalNumbers:=True))

            ' "well-known" characters:
            Assert.Equal("""a"" & vbBack", FormatPrimitiveUsingHexadecimalNumbers("a" & vbBack, quoteStrings:=True))
            Assert.Equal("""a"" & vbCr", FormatPrimitiveUsingHexadecimalNumbers("a" & vbCr, quoteStrings:=True))
            Assert.Equal("""a"" & vbCrLf", FormatPrimitiveUsingHexadecimalNumbers("a" & vbCrLf, quoteStrings:=True))
            Assert.Equal("""a"" & vbFormFeed", FormatPrimitiveUsingHexadecimalNumbers("a" & vbFormFeed, quoteStrings:=True))
            Assert.Equal("""a"" & vbLf", FormatPrimitiveUsingHexadecimalNumbers("a" & vbLf, quoteStrings:=True))
            Assert.Equal("""a"" & vbNullChar", FormatPrimitiveUsingHexadecimalNumbers("a" & vbNullChar, quoteStrings:=True))
            Assert.Equal("""a"" & vbTab", FormatPrimitiveUsingHexadecimalNumbers("a" & vbTab, quoteStrings:=True))
            Assert.Equal("""a"" & vbVerticalTab", FormatPrimitiveUsingHexadecimalNumbers("a" & vbVerticalTab, quoteStrings:=True))
        End Sub

        <Fact(), WorkItem(529850)>
        Public Sub CultureInvariance()
            Dim originalCulture = CurrentThread.CurrentCulture
            Try
                CurrentThread.CurrentCulture = New CultureInfo(1031) ' de-DE

                Dim dateValue As New Date(2001, 1, 31)
                Assert.Equal("31.01.2001 00:00:00", dateValue.ToString())
                Assert.Equal("#1/31/2001 12:00:00 AM#", FormatPrimitive(dateValue))

                Dim decimalValue As New Decimal(12.5)
                Assert.Equal("12,5", decimalValue.ToString())
                Assert.Equal("12.5", FormatPrimitive(decimalValue))

                Dim doubleValue As Double = 12.5
                Assert.Equal("12,5", doubleValue.ToString())
                Assert.Equal("12.5", FormatPrimitive(doubleValue))

                Dim singleValue As Single = 12.5
                Assert.Equal("12,5", singleValue.ToString())
                Assert.Equal("12.5", FormatPrimitive(singleValue))
            Finally
                CurrentThread.CurrentCulture = originalCulture
            End Try
        End Sub

        Private Function FormatPrimitive(obj As Object, Optional quoteStrings As Boolean = False) As String
            Return ObjectDisplay.FormatPrimitive(obj, quoteStrings, useHexadecimalNumbers:=False)
        End Function
        Private Function FormatPrimitiveUsingHexadecimalNumbers(obj As Object, Optional quoteStrings As Boolean = False) As String
            Return ObjectDisplay.FormatPrimitive(obj, quoteStrings, useHexadecimalNumbers:=True)
        End Function

    End Class

End Namespace