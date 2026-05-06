import gg.xp.xivdata.data.ActionInfo;
import gg.xp.xivdata.data.ActionLibrary;

import java.io.BufferedWriter;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Map;

public final class ExportTriggeventActions {
    public static void main(String[] args) throws Exception {
        if (args.length != 1) {
            System.err.println("usage: java ExportTriggeventActions.java <output.csv>");
            System.exit(2);
        }

        Path output = Path.of(args[0]);
        Files.createDirectories(output.getParent());

        try (BufferedWriter writer = Files.newBufferedWriter(output, StandardCharsets.UTF_8)) {
            writer.write("id,name,original_name,cast_time,recast_time,is_player_ability,category_raw");
            writer.newLine();
            ActionLibrary.getAll().entrySet().stream()
                    .sorted(Map.Entry.comparingByKey())
                    .forEach(entry -> writeRow(writer, entry.getValue()));
        }
    }

    private static void writeRow(BufferedWriter writer, ActionInfo action) {
        try {
            writer.write(Long.toString(action.actionid()));
            writer.write(',');
            writer.write(csv(action.name()));
            writer.write(',');
            writer.write(csv(action.originalName()));
            writer.write(',');
            writer.write(Double.toString(action.getCastTime()));
            writer.write(',');
            writer.write(Double.toString(action.getCd()));
            writer.write(',');
            writer.write(Boolean.toString(action.isPlayerAbility()));
            writer.write(',');
            writer.write(Integer.toString(action.actionCategoryRaw()));
            writer.newLine();
        }
        catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    private static String csv(String value) {
        if (value == null) {
            return "";
        }
        boolean needsQuotes = value.indexOf(',') >= 0
                || value.indexOf('"') >= 0
                || value.indexOf('\n') >= 0
                || value.indexOf('\r') >= 0;
        if (!needsQuotes) {
            return value;
        }
        return '"' + value.replace("\"", "\"\"") + '"';
    }
}
