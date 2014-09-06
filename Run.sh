#!/bin/sh

function vsvers()
{
	if [ "$VS120COMNTOOLS" ]; then
		echo " /property:VisualStudioVersion=12.0"
	else
		echo ""
	fi
}

$WINDIR/Microsoft.NET/Framework/v4.0.30319/MSBuild.exe build/Build.proj -v:minimal -maxcpucount -nodeReuse:false `vsvers` $*
