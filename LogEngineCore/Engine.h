#pragma once


extern "C" {
#include "../include/Parser.h"
}


class LogEngine {
private:
	LogEntry* m_entries;
	int m_entryCount;

public:
	LogEngine() : m_entries(nullptr), m_entryCount(0) {}

	~LogEngine() {
		if (m_entries) {
			free_log_entrys(m_entries);
		}
	}

	bool LoadFile(const char* filepath) {
		m_entries = load_log_file(filepath, &m_entryCount);
		return m_entries != nullptr;
	}

	int GetCount() const { return m_entryCount; }

	const LogEntry* GetEntry(int index) const {
		if (index >= 0 && index < m_entryCount) {
			return &m_entries[index];
		}
		return nullptr;
	}

};

