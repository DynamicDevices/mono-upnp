
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG" "-keyfile:$(srcdir)/mono-upnp-dcp-mediaserver1-tests.snk"
ASSEMBLY = ../../bin/Mono.Upnp.Dcp.MediaServer1.Tests.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Upnp.Dcp.MediaServer1.dll \
	../../bin/Mono.Upnp.dll
BUILD_DIR = ../../bin

MONO_UPNP_DCP_MEDIASERVER1_TESTS_DLL_MDB_SOURCE=../../bin/Mono.Upnp.Dcp.MediaServer1.Tests.dll.mdb
MONO_UPNP_DCP_MEDIASERVER1_TESTS_DLL_MDB=$(BUILD_DIR)/Mono.Upnp.Dcp.MediaServer1.Tests.dll.mdb
MONO_UPNP_DCP_MEDIASERVER1_DLL_SOURCE=../../bin/Mono.Upnp.Dcp.MediaServer1.dll
MONO_UPNP_DLL_SOURCE=../../bin/Mono.Upnp.dll
MONO_SSDP_DLL_SOURCE=../../bin/Mono.Ssdp.dll
MONO_SSDP_DLL_MDB_SOURCE=../../bin/Mono.Ssdp.dll.mdb
MONO_SSDP_DLL_MDB=$(BUILD_DIR)/Mono.Ssdp.dll.mdb
MONO_UPNP_DLL_MDB_SOURCE=../../bin/Mono.Upnp.dll.mdb
MONO_UPNP_DLL_MDB=$(BUILD_DIR)/Mono.Upnp.dll.mdb
MONO_UPNP_DCP_MEDIASERVER1_DLL_MDB_SOURCE=../../bin/Mono.Upnp.Dcp.MediaServer1.dll.mdb
MONO_UPNP_DCP_MEDIASERVER1_DLL_MDB=$(BUILD_DIR)/Mono.Upnp.Dcp.MediaServer1.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = mcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- "-keyfile:$(srcdir)/mono-upnp-dcp-mediaserver1-tests.snk"
ASSEMBLY = ../../bin/Mono.Upnp.Dcp.MediaServer1.Tests.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Upnp.Dcp.MediaServer1.dll \
	../../bin/Mono.Upnp.dll
BUILD_DIR = ../../bin

MONO_UPNP_DCP_MEDIASERVER1_TESTS_DLL_MDB=
MONO_UPNP_DCP_MEDIASERVER1_DLL_SOURCE=../../bin/Mono.Upnp.Dcp.MediaServer1.dll
MONO_UPNP_DLL_SOURCE=../../bin/Mono.Upnp.dll
MONO_SSDP_DLL_SOURCE=../../bin/Mono.Ssdp.dll
MONO_SSDP_DLL_MDB=
MONO_UPNP_DLL_MDB=
MONO_UPNP_DCP_MEDIASERVER1_DLL_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONO_UPNP_DCP_MEDIASERVER1_TESTS_DLL_MDB) \
	$(MONO_UPNP_DCP_MEDIASERVER1_DLL) \
	$(MONO_UPNP_DLL) \
	$(MONO_SSDP_DLL) \
	$(MONO_SSDP_DLL_MDB) \
	$(MONO_UPNP_DLL_MDB) \
	$(MONO_UPNP_DCP_MEDIASERVER1_DLL_MDB)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) 

FILES =  \
	Consumer.cs \
	DummyContentDirectory.cs \
	DummyObject.cs \
	FilteringXmlSerializerTests.cs \
	ObjectQueryContextTests.cs \
	ObjectQueryTests.cs \
	ObjectSerializationTests.cs \
	ObjectTests.cs \
	QueryParserTests.cs \
	QueryTests.cs \
	UpdateTextWriterTests.cs \
	UpdateXmlSerializerTests.cs \
	VirtualXmlSerializerTests.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	mono-upnp-dcp-mediaserver1-tests.snk 

REFERENCES =  \
	../../bin/Mono.Upnp.Dcp.MediaServer1.dll \
	../../bin/Mono.Upnp.dll \
	-pkg:nunit \
	System \
	System.Xml

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) 

include $(top_srcdir)/Makefile.include

MONO_UPNP_DCP_MEDIASERVER1_DLL = $(BUILD_DIR)/Mono.Upnp.Dcp.MediaServer1.dll
MONO_UPNP_DLL = $(BUILD_DIR)/Mono.Upnp.dll
MONO_SSDP_DLL = $(BUILD_DIR)/Mono.Ssdp.dll



$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
