<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="ArrayOfLoanApplicationBll">
    <HTML>
      <BODY>
        <TABLE BORDER="2">
          <TR>
            <TD>Application Number</TD>
            <TD>Application Status</TD>
            <TD>Applicant Details</TD>
            <TD>Amount Requested</TD>
            <TD>Amount Granted</TD>
            <TD>Date Of Submission</TD>
          </TR>
          <xsl:apply-templates select="LoanApplicationBll"/>
        </TABLE>
      </BODY>
    </HTML>
  </xsl:template>
  <xsl:template match="LoanApplicationBll">
    <TR>
      <TD>
        <xsl:value-of select="ApplicationNumber"/>
      </TD>
      <TD>
        <xsl:value-of select="ApplicantDetails"/>
      </TD>
      <TD>
        <xsl:value-of select="AmountRequested"/>
      </TD>
      <TD>
        <xsl:value-of select="AmountGranted"/>
      </TD>
      <TD>
        <xsl:value-of select="DateOfSubmission"/>
      </TD>
      <TD>
        <xsl:value-of select="ApplicationStatus"/>
      </TD>
    </TR>
  </xsl:template>
</xsl:stylesheet>
