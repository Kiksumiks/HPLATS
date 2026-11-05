#pragma once


extern "C" {
#include "../Parser/Parser.h"
}


class LogEngine {
private:
	char* m_fileBuffer;
	LogEntry* m_entries;
	int m_entryCount;

public:
	LogEngine() : m_fileBuffer(nullptr), m_entries(nullptr), m_entryCount(0) {}
	~LogEngine() {	free_log_entrys(m_entries, m_fileBuffer); }

	bool LoadFile(const char* filepath) {
		m_entries = load_log_file(filepath, &m_entryCount, m_fileBuffer);
		return m_entries != nullptr;
	}

	int GetCount() const { return m_entryCount; }

	/*
		const LogEntry* GetEntry(int index) const {
		if (index >= 0 && index < m_entryCount) {
			return &m_entries[index];
		}
		return nullptr;
	}
	*/

	LogEntry* GetEntries() const {
		return m_entries;
	}

};

