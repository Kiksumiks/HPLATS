#define _CRT_SECURE_NO_WARNINGS
#include "../include/Parser.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// Get rid of this function. Rather do single file read and use realloc!
// Get rid of this function. Rather do single file read and use realloc!
// Get rid of this function. Rather do single file read and use realloc!
int count_lines_in_file(const char* filepath) {
    char buffer[512];
    int lineCount = 0;

    FILE* file = fopen(filepath, "r");
    if (file == NULL) {
        printf("Failed to open file from path '%s'\n", filepath);
        return 0;
    }

    while (fgets(buffer, 512, file))
        lineCount++;

    fclose(file);

    return lineCount;
}

LogEntry* load_log_file(const char* filepath, int* out_count) {

    // Get line/entry count
    int entryCount = count_lines_in_file(filepath);
    if (entryCount == 0)
        return NULL;

    // Open file
    FILE* file = fopen(filepath, "r");
    if (file == NULL) {
        printf("Failed to open file from path '%s'\n", filepath);
        fclose(file);
        return NULL;
    }

    LogEntry* entries = (LogEntry*)malloc(sizeof(LogEntry) * entryCount);
    if(entries == NULL) {
        fclose(file);
        return NULL;
    }

    char buffer[512];
    int index = 0;
    while (fgets(buffer, 512, file)) {
        int result = parse_log_line(buffer, entries + index);
        if(result == 1)
            index++;
    }

    fclose(file);

    *out_count = index;
    
    return entries;
}

int parse_log_line(const char* line, LogEntry* out_entry) {

    char buffer[512];
    strncpy(buffer, line, 512);

    char* ptr = strtok(buffer, " |");
    if (ptr == NULL) {
        return 0;
    }

    char* array[5] = { 0 };

    for (int i = 0; ptr != NULL; i++) {
        array[i] = ptr;
        if (i >= 3) break;
        ptr = strtok(NULL, " |");
    }

    ptr = strtok(NULL, "|");
    array[4] = ptr;

    if (array[0] == NULL || array[1] == NULL || array[2] == NULL || array[3] == NULL || array[4] == NULL)
        return 0;

    strncpy(out_entry->date, array[0], 11);
    strncpy(out_entry->time, array[1], 9);
    strncpy(out_entry->level, array[2], 8);
    strncpy(out_entry->thread_id, array[3], 16);
    strncpy(out_entry->message, array[4] + 1, 256); // + 1 removes leading whitespace

    return 1;
}

void free_log_entrys(LogEntry* entrys) {
    if (entrys != NULL) {
        free(entrys);
        entrys = NULL;
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

    char logPath[] = "../Example-Log.txt";
    int entryCount = 0;
    LogEntry* entries = load_log_file(logPath, &entryCount);

    for (int i = 0; i < entryCount; i++) {
        printf("%s\n%s\n%s\n%s\n%s\n\n", entries[i].date, entries[i].time, entries[i].level, entries[i].thread_id, entries[i].message);
    }

    free_log_entrys(entries);

    return 0;
}