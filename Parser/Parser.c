#define _CRT_SECURE_NO_WARNINGS
#include "Parser.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>


LogEntry* load_log_file(const char* filepath, int* out_count, char* file_buffer) {

    FILE* file = fopen(filepath, "r");
    if (!file) return NULL;

    fseek(file, 0, SEEK_END);
    long file_size = ftell(file);
    fseek(file, 0, SEEK_SET);

    file_buffer = (char*)malloc(file_size + 1);
    if(!file_buffer) {
        fclose(file);
        return NULL;
    }

    fread(file_buffer, 1, file_size, file);
    file_buffer[file_size] = '\0';
    fclose(file);

    int currentArraySize = 10000;
    LogEntry* entries = (LogEntry*)malloc(sizeof(LogEntry) * currentArraySize);
    if(entries == NULL) {
        fclose(file);
        return NULL;
    }

    int index = 0;
    char* line = strtok(file_buffer, "\n");
    while (line != NULL) {
        if (parse_log_line(line, &entries[index])) {
            index++;

            if (index >= currentArraySize) {
                currentArraySize *= 2;
                LogEntry* tempEntries = (LogEntry*)realloc(entries, sizeof(LogEntry) * currentArraySize);
                if (tempEntries == NULL) {
                    free(file_buffer);
                    free(entries);
                    return NULL;
                }
                entries = tempEntries;
            }
        }

        line = strtok(NULL, "\n");
    }

    *out_count = index;
    return entries;
}

int parse_log_line(char* line, LogEntry* out_entry) {

    if (!line || *line == '\n' || *line == '\0') return 0;

    out_entry->date = line;
    line[10] = '\0';

    out_entry->time = line + 11;
    line[19] = '\0';

    char* level_ptr = line + 22;
    out_entry->level = level_ptr;

    char* next_delim = strstr(level_ptr, " | ");
    if(!next_delim) return 0;
    *next_delim = '\0';

    char* thread_ptr = next_delim + 3;
    out_entry->thread_id = thread_ptr;

    next_delim = strstr(thread_ptr, " | ");
    if(!next_delim) return 0;
    *next_delim = '\0';

    out_entry->message = next_delim + 3;

    return 1;
}

void free_log_entrys(LogEntry* entrys, char* file_buffer) {
    if (entrys != NULL) {
        free(entrys);
        entrys = NULL;
    }

    if (file_buffer != NULL) {
        free(file_buffer);
        file_buffer = NULL;
    }
}

int main() {

    // Test parse_log_line()
    /*
    char logText[] = "2026-10-20 08:15:01 | INFO   | Thread-1 | System initialization started successfully.\0";
    LogEntry* entry = (LogEntry*)malloc(sizeof(LogEntry));
    if (entry == NULL) {
        printf("Failed to malloc!\n");
        return 1;
    }

    int result = parse_log_line(logText, entry);
    if (result == 1) {
        printf("%s\n%s\n%s\n%s\n%s\n", entry->date, entry->time, entry->level, entry->thread_id, entry->message);
    }

    free_log_entry(entry);
    */

    char logPath[] = "../heavy_test_log.txt";

    //char logPath[] = "../Example-Log.txt";

    int entryCount = 0;

    char* fileBuffer = NULL;
    
    clock_t begin = clock();

    LogEntry* entries = load_log_file(logPath, &entryCount, fileBuffer);

    clock_t end = clock();

    double timeSpend = (double)(end - begin) / CLOCKS_PER_SEC;

    //for (int i = 0; i < entryCount; i++) {
        //printf("%s %s %s %s %s\n", entries[i].date, entries[i].time, entries[i].level, entries[i].thread_id, entries[i].message);
    //}

    printf("Read %d log entries in %lf seconds.\n", entryCount, timeSpend);

    free_log_entrys(entries, fileBuffer);

    return 0;
}