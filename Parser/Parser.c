#define _CRT_SECURE_NO_WARNINGS
#include "Parser.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

char* readFileIntoBuffer(const char* filepath) {

    FILE* file = fopen(filepath, "r");
    if (!file) return NULL;

    fseek(file, 0, SEEK_END);
    long fileSize = ftell(file);
    fseek(file, 0, SEEK_SET);

    char* fileBuffer = (char*)malloc(fileSize + 1);
    if(!fileBuffer) {
        fclose(file);
        return NULL;
    }

    size_t readBytes = fread(fileBuffer, 1, fileSize, file);
    fileBuffer[readBytes] = '\0';
    fclose(file);

    return fileBuffer;
}


LogEntry* load_log_file(int* out_count, char* file_buffer) {

    if(!file_buffer) return NULL;

    int currentArraySize = 10000;

    LogEntry* entries = (LogEntry*)malloc(sizeof(LogEntry) * currentArraySize);
    if(entries == NULL) return NULL;

    int index = 0;
    char* currentLine = file_buffer;
    char* nextLine = NULL;

    while (currentLine && *currentLine != '\0') {

        nextLine = strchr(currentLine, '\n');
        if( nextLine) {
            *nextLine = '\0';
            nextLine++;
        }

        if (parse_log_line(currentLine, &entries[index])) {
            index++;

            if (index >= currentArraySize) {
                currentArraySize *= 2;
                LogEntry* tempEntries = (LogEntry*)realloc(entries, sizeof(LogEntry) * currentArraySize);
                if (tempEntries == NULL) {
                    free(entries);
                    return NULL;
                }
                entries = tempEntries;
            }
        }
        currentLine = nextLine;    
    }

    *out_count = index;
    return entries;
}

int parse_log_line(char* line, LogEntry* out_entry) {

    if (!line || *line == '\n' || *line == '\0') return 0;
    if (strlen(line) < 22) return 0;

    out_entry->date = line;
     
    out_entry->time = line + 11;

    line[10] = '\0';
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
    if (entrys != NULL) free(entrys);
    if (file_buffer != NULL) free(file_buffer);
}

int main() {

    //char logPath[] = "../realistic_test.txt";
    char logPath[] = "../heavy_test_log.txt";
    //char logPath[] = "../Example-Log.txt";
    //char logPath[] = "../test.txt";

    int entryCount = 0;
    char* fileBuffer = readFileIntoBuffer(logPath);
    
    clock_t begin = clock();
    LogEntry* entries = load_log_file(&entryCount, fileBuffer);
    clock_t end = clock();

    double timeSpend = (double)(end - begin) / CLOCKS_PER_SEC;

    printf("Read %d log entries in %lf seconds.\n", entryCount, timeSpend);

    free_log_entrys(entries, fileBuffer);

    return 0;
}